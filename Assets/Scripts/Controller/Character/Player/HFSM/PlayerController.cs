using System;
using Controller.Character.Player.Player.State.Ground_State;
using QFramework;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Input_System;
using UnityEngine;
using UnityEngine.Events;

namespace Controller.Character.Player.Player
{
    public partial class PlayerController : MonoBehaviour
    {
        private bool _applyRootMotion;

        public StateMachine<Type, Type, Type> FSM { get; private set; }

        #region Callback

        [HideInInspector]
        public UnityEvent onUpdate = new UnityEvent();

        #endregion
        
        private void Awake()
        {
            FSM = new StateMachine<Type, Type, Type>();
            
            var attackTime = 0;
            var comboFlag = false;
            
            FSM.AddState<MoveState>(
                animator,
                "Ground",
                onEnter: state =>
                {
                    _applyRootMotion = false;
                    
                    InputKit.Instance.attack.Reset();
                    comboFlag = false;
                },
                onFixedLogic: state =>
                {
                    #region Move

                    var movement = InputKit.Instance.move.Value.normalized;
                    var targetSpeed = movement.magnitude * 
                                      (InputKit.Instance.run ? config.runSpeed : config.walkSpeed);
                    var currentSpeed = animator.GetFloat(SpeedXZParam);
                    var nextSpeed = 0.1f.Lerp(currentSpeed, targetSpeed);
                    
                    animator.SetFloat(SpeedXZParam, nextSpeed);

                    #endregion
                    
                    #region Slop Detect

                    var isRaycastHitFront = Physics.Raycast(
                        transform.position + Vector3.up + config.slopDetectFrontOffset * transform.forward, 
                        Vector3.down, 
                        out var frontHit, 
                        config.slopDetectFrontDistance, 
                        config.groundLayerMask);
                    var frontAngle = isRaycastHitFront ? Vector3.Angle(frontHit.normal, Vector3.up) : float.MaxValue;
            
                    var isRaycastHitDown = Physics.Raycast(
                        transform.position + Vector3.up, 
                        Vector3.down, 
                        out var downHit, 
                        config.groundDetectDistance, 
                        config.groundLayerMask);
                    var downAngle = isRaycastHitDown ? Vector3.Angle(downHit.normal, Vector3.up) : float.MaxValue;

                    var slopeNormalPerp = 
                        isRaycastHitFront ? Vector3.Cross(transform.right, frontHit.normal) : transform.forward; 
                    // 在空中就直接向前，不做额外处理
            
                    var velocity = animator.velocity.magnitude * slopeNormalPerp;
                    velocity.y = isRaycastHitDown ? velocity.y : rb.velocity.y; // 如果在空中，就不改变 velocity.y
            
                    if (downAngle < config.flatGroundMaxAngle && velocity.y < 0f) 
                        // 在平面上且下一瞬的速度是向下的
                    {
                        velocity.y = 0f;
                    }

                    var deltaForce = velocity - rb.velocity;
                    
                    rb.AddForce(deltaForce, ForceMode.VelocityChange);
                    
                    #endregion

                    #region Physical Material

                    moveCollider.material = downAngle < config.flatGroundMaxAngle || 
                                            frontAngle > config.slopMaxAngle || 
                                            InputKit.Instance.move.Value.magnitude > 0.1f ? 
                        config.zeroFrictionMat : config.fullFrictionMat;

                    #endregion
                    
                    #region Rotate

                    if (InputKit.Instance.move.Value.magnitude >= 0.1f)
                    {
                        var right = cam.transform.right;
                        var inputDirection = InputKit.Instance.move.Value.normalized;
                        var camForward = Vector3.Cross(right, Vector3.up);
                        var targetDir = right * inputDirection.x + camForward * inputDirection.y;
                        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
                    }

                    #endregion
                },
                onExit: state => _applyRootMotion = true
            );
            
            FSM.AddState<JumpState>(
                animator,
                "Jump",
                onEnter: state =>
                {
                    _applyRootMotion = false;

                    moveCollider.material = config.zeroFrictionMat;
                    
                    InputKit.Instance.jump.Reset();
					
                    rb.AddForce(new Vector3(0, config.jumpForce, 0), ForceMode.Impulse);
                },
                onExit: state =>
                {
                    _applyRootMotion = true;
                },
                canExit: state => state.timer.IsAnimatorFinish,
                needsExitTime: true
            );
            
            FSM.AddState<FallState>(
                animator,
                "Fall",
                onEnter: state =>
                {
                    _applyRootMotion = false;
                },
                onExit: state =>
                {
                    _applyRootMotion = true;
                }
            );
            
            FSM.AddState<LandState>(
                animator,
                "Land",
                canExit: state => state.timer.IsAnimatorFinish,
                needsExitTime: true
            );
            
            FSM.AddState<AttackState>(
                animator,
                "Attack Select",
                onEnter: state =>
                {
                    InputKit.Instance.attack.Reset();

                    var velocity = new Vector3(0, rb.velocity.y, 0) ;
                    rb.velocity = velocity;
                    
                    attackTime = comboFlag ? (attackTime + 1) % 3 : 0;
                    comboFlag = false;
                    
                    animator.SetInteger(Attack, attackTime);
                },
                canExit: state => state.timer > 1.2f,
                needsExitTime: true
            );
            
            FSM.AddState<BlockState>(
                animator,
                "Block",
                onEnter: state =>
                {
                    InputKit.Instance.block.Reset();
                    
                    var velocity = new Vector3(0, rb.velocity.y, 0) ;
                    rb.velocity = velocity;
                },
                canExit: state => state.timer > config.blockTime,
                needsExitTime: true
            );
            
            FSM.AddState<DodgeState>(
                animator,
                "Dodge",
                onEnter: state =>
                {
                    InputKit.Instance.dodge.Reset();
                    
                    var velocity = new Vector3(0, rb.velocity.y, 0) ;
                    rb.velocity = velocity;
                },
                canExit: state => state.timer.IsAnimatorFinish,
                needsExitTime: true
            );
            
            FSM.AddTransition<MoveState, DodgeState>
                (transition => InputKit.Instance.dodge);
            
            FSM.AddTransition<MoveState, JumpState>
                (transition => InputKit.Instance.jump);
			         
            FSM.AddTransition<MoveState, FallState>
                (transition => !sensorController.groundSensor);
            
            FSM.AddTransition<MoveState, AttackState>
                (transition => InputKit.Instance.attack);
            
            FSM.AddTransition<MoveState, BlockState>
                (transition => InputKit.Instance.block);
            
            FSM.AddTransition<JumpState, FallState>
                (transition => true);
            
            FSM.AddTransition<JumpState, MoveState>
                (transition => sensorController.groundSensor);
            
            FSM.AddTransition<FallState, MoveState>
                (transition => sensorController.groundSensor);
            
            FSM.AddTransition<AttackState, MoveState>
                (transition => InputKit.Instance.move.Value.magnitude > 0.1f);
            
            FSM.AddTransition<AttackState, AttackState>
            (transition => InputKit.Instance.attack,
                onTransition: transition => comboFlag = true);
            
            FSM.AddTransition<BlockState, MoveState>
                (transition => true);
            
            FSM.AddTransition<DodgeState, MoveState>
                (transition => true);
        }

        private void Start()
        {
            FSM.Init();
        }

        private void Update()
        {
            FSM.OnLogic();
            
            onUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            FSM.OnFixedLogic();
        }

        private void OnAnimatorMove()
        {
            if (!_applyRootMotion)
            {
                return;
            }
            
            var velocity = animator.velocity;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }
    }
}
