using System;
using Controller.Character.Player.Player.State.Ground_State;
using QFramework;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Input_System;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    public partial class PlayerController : MonoBehaviour
    {
        private bool _applyRootMotion;
        
        public StateMachine<Type, Type, Type> FSM { get; private set; }

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            FSM = new StateMachine<Type, Type, Type>();
            
            FSM.AddState<MoveState>(
                animator,
                "Ground",
                onLogic: state =>
                {
                    #region Move

                    var movement = InputKit.Instance.move.Value.normalized;
                    var targetSpeed = movement.magnitude * 
                                      (InputKit.Instance.run ? config.runSpeed : config.walkSpeed);
                    var currentSpeed = animator.GetFloat(SpeedXZParam);
                    var nextSpeed = 0.05f.Lerp(currentSpeed, targetSpeed);
                    
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
            
                    rb.velocity = velocity;

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
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.05f);
                    }

                    #endregion
                    
                }
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
            
            FSM.AddTransition<MoveState, JumpState>
                (transition => InputKit.Instance.jump);
			         
            FSM.AddTransition<MoveState, FallState>
                (transition => !sensorController.groundSensor);
            
            FSM.AddTransition<JumpState, FallState>
                (transition => true);
            
            FSM.AddTransition<JumpState, MoveState>
                (transition => sensorController.groundSensor);
            
            FSM.AddTransition<FallState, MoveState>
                (transition => sensorController.groundSensor);
        }

        private void Start()
        {
            FSM.Init();
        }

        private void Update()
        {
            FSM.OnLogic();
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
