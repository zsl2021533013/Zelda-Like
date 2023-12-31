using System;
using Command;
using Controller.Character.Player.Player.State.Ground_State;
using Data.Combat;
using DG.Tweening;
using Model;
using Model.Interface;
using QFramework;
using QFramework.Example;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Input_System;
using UniRx;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    public partial class PlayerController : MonoBehaviour, IController
    {
        private bool _applyRootMotion;

        public StateMachine<Type, Type, Type> FSM { get; private set; }

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
                    
                    #endregion

                    #region Physical Material

                    moveCollider.material = downAngle < config.flatGroundMaxAngle || 
                                            frontAngle > config.slopMaxAngle || 
                                            InputKit.Instance.move.Value.magnitude > 0.1f ? 
                        config.zeroFrictionMat : config.fullFrictionMat;

                    #endregion
                    
                    rb.AddForce(deltaForce, ForceMode.VelocityChange);
                    
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
                onExit: state =>
                {
                    animator.SetFloat(SpeedXZParam, 0f);
                    _applyRootMotion = true;
                });
            
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
                onFixedLogic: state =>
                {
                    #region Move
                    
                    var movement = InputKit.Instance.move.Value.normalized;
                    var targetSpeed = movement.magnitude * config.airMoveSpeed;
                    var currentSpeedXZ = rb.velocity;
                    currentSpeedXZ.y = 0f;
                    var nextSpeed = 0.1f.Lerp(currentSpeedXZ.magnitude, targetSpeed);
                    
                    #endregion

                    var velocity = nextSpeed * transform.forward;
                    velocity.y = rb.velocity.y;

                    var deltaForce = velocity - rb.velocity;

                    rb.AddForce(deltaForce, ForceMode.VelocityChange);
                    
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
                },
                onFixedLogic: state =>
                {
                    #region Move
                    
                    var movement = InputKit.Instance.move.Value.normalized;
                    var targetSpeed = movement.magnitude * config.airMoveSpeed;
                    var currentSpeedXZ = rb.velocity;
                    currentSpeedXZ.y = 0f;
                    var nextSpeed = 0.1f.Lerp(currentSpeedXZ.magnitude, targetSpeed);
                    
                    #endregion

                    var velocity = nextSpeed * transform.forward;
                    velocity.y = rb.velocity.y;

                    var deltaForce = velocity - rb.velocity;

                    rb.AddForce(deltaForce, ForceMode.VelocityChange);
                    
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
                }
            );
            
            FSM.AddState<LandState>(
                animator,
                "Land",
                canExit: state => state.timer.IsAnimatorFinish,
                needsExitTime: true
            );

            Transform closestEnemy = null;
            FSM.AddState<AttackState>(
                animator,
                () =>
                {
                    attackTime = comboFlag ? (attackTime + 1) % 3 : 0;
                    comboFlag = false;
                    return $"Attack {attackTime}";
                },
                onEnter: state =>
                {
                    InputKit.Instance.attack.Reset();

                    var velocity = new Vector3(0, rb.velocity.y, 0) ;
                    rb.velocity = velocity;

                    closestEnemy = sensorController.GetAttackTarget();
                },
                onFixedLogic: state =>
                {
                    if (closestEnemy == null)
                    {
                        return;
                    }
                    
                    var targetDir = (closestEnemy.position - transform.position);
                    targetDir.y = 0f;
                    targetDir.Normalize();
                    var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.3f);
                },
                onExit: state => weaponController.CloseWeapon(),
                canExit: state => state.timer > 0.65f,
                needsExitTime: true
            );
            
            FSM.AddState<ParryState>(
                animator,
                "Parry",
                onEnter: state =>
                {
                    InputKit.Instance.parry.Reset();
                    
                    var velocity = new Vector3(0, rb.velocity.y, 0) ;
                    rb.velocity = velocity;
                    
                    closestEnemy = sensorController.GetAttackTarget();
                },
                onFixedLogic: state =>
                {
                    if (closestEnemy == null)
                    {
                        return;
                    }
                    
                    var targetDir = (closestEnemy.position - transform.position);
                    targetDir.y = 0f;
                    targetDir.Normalize();
                    var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.3f);
                },
                canExit: state => state.timer > 0.7f,
                needsExitTime: true
            );
            
            var abilityType = PlayerModel.AbilityType.Fireball;
            FSM.AddState<SpecialAbilityState>(
                animator,
                "Focus",
                onEnter: state =>
                {
                    _applyRootMotion = false;
                    animator.ResetTrigger(Boost);

                    if (InputKit.Instance.fireball)
                    {
                        abilityType = PlayerModel.AbilityType.Fireball;
                    }
                    else if (InputKit.Instance.timeStop)
                    {
                        abilityType = PlayerModel.AbilityType.TimeStop;
                    }

                    UIKit.OpenPanel<CrossHairPanel>();
                },
                onFixedLogic: state =>
                {
                    #region Move

                    var input = InputKit.Instance.move.Value;
                    var movement = input.normalized;
                    var targetSpeedX = movement.x;
                    var targetSpeedZ = movement.y;
                    var currentSpeedX = animator.GetFloat(SpeedXParam);
                    var currentSpeedZ = animator.GetFloat(SpeedZParam);
                    var nextSpeedX = 0.1f.Lerp(currentSpeedX, targetSpeedX);
                    var nextSpeedZ = 0.1f.Lerp(currentSpeedZ, targetSpeedZ);
                    
                    animator.SetFloat(SpeedXParam, nextSpeedX);
                    animator.SetFloat(SpeedZParam, nextSpeedZ);
                    
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
                    
                    var inputAngle = Vector2.SignedAngle(input, Vector2.up);
                    var playerAngle = transform.eulerAngles.y;
                    var rotateQuat = Quaternion.Euler(0f, inputAngle + playerAngle, 0f);
                    var rightQuat = Quaternion.Euler(0f, -90f, 0f);
                    var right = rightQuat * rotateQuat * Vector3.forward;
                    
                    var slopeNormalPerp = 
                        isRaycastHitFront ? Vector3.Cross(frontHit.normal, right) : transform.forward; 
                    // 在空中就直接向前，不做额外处理
                    
                    var velocity = animator.velocity.magnitude * slopeNormalPerp;
                    velocity.y = isRaycastHitDown ? velocity.y : rb.velocity.y; // 如果在空中，就不改变 velocity.y
                    
                    if (downAngle < config.flatGroundMaxAngle && velocity.y < 0f) 
                        // 在平面上且下一瞬的速度是向下的
                    {
                        velocity.y = 0f;
                    }

                    var deltaForce = velocity - rb.velocity;
                    
                    #endregion

                    #region Physical Material

                    moveCollider.material = downAngle < config.flatGroundMaxAngle || 
                                            frontAngle > config.slopMaxAngle || 
                                            InputKit.Instance.move.Value.magnitude > 0.1f ? 
                        config.zeroFrictionMat : config.fullFrictionMat;
                    
                    #endregion
                    
                    rb.AddForce(deltaForce, ForceMode.VelocityChange);
                    
                    #region Rotate

                    var targetRot = new Vector3(0f, cam.transform.eulerAngles.y, 0f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRot), 0.1f);

                    #endregion
                },
                onExit: state =>
                {
                    UIKit.ClosePanel<CrossHairPanel>();
                    
                    if (abilityType == PlayerModel.AbilityType.Fireball)
                    {
                        this.SendCommand(new SpawnPlayerFireballCommand()
                        {
                            position = cam.transform.position + 2 * cam.transform.forward,
                            rotation = cam.transform.rotation,
                            target = sensorController.FocusCameraRaycast()
                        });
                    }
                    else if (abilityType == PlayerModel.AbilityType.TimeStop)
                    {
                        this.SendCommand(new TimeStopCommand());
                            
                        Observable
                            .Timer(TimeSpan.FromSeconds(config.timeStopDuration))
                            .Subscribe(_ => this.SendCommand(new TimeResetCommand()))
                            .AddTo(this);
                    }

                    animator.SetFloat(SpeedXParam, 0f);
                    animator.SetFloat(SpeedZParam, 0f);

                    _applyRootMotion = true;
                });
            
            FSM.AddState<StabState>(
                animator,
                "Stab",
                onEnter: state =>
                {
                    InputKit.Instance.attack.Reset();

                    var enemy = sensorController.backStabSensor.Value.transform;
                    
                    DOTween.Sequence()
                        .Append(transform.DOMove(enemy.position + enemy.forward, 0.2f))
                        .Append(transform.DOLookAt(enemy.position, 0.2f));
                    
                    var velocity = new Vector3(0, rb.velocity.y, 0) ;
                    rb.velocity = velocity;

                    this.SendCommand(new StabEnemyCommand() { enemy = enemy });
                    
                    this.SendCommand(new TryHurtEnemyCommand()
                    {
                        enemy = enemy,
                        attackerData = this.GetModel<IPlayerModel>().components.Get<CharacterCombatData>()
                    });
                },
                canExit: state => state.timer.IsAnimatorFinish,
                needsExitTime: true
            );
            
            FSM.AddState<BackStabState>(
                animator,
                "Back Stab",
                onEnter: state =>
                {
                    InputKit.Instance.attack.Reset();

                    var enemy = sensorController.backStabSensor.Value.transform;
                    
                    DOTween.Sequence()
                        .Append(transform.DOMove(enemy.position - enemy.forward, 0.2f))
                        .Append(transform.DORotate(enemy.rotation.eulerAngles, 0.2f));
                    
                    var velocity = new Vector3(0, rb.velocity.y, 0) ;
                    rb.velocity = velocity;

                    this.SendCommand(new BackStabEnemyCommand() { enemy = enemy });
                    
                    this.SendCommand(new TryHurtEnemyCommand()
                    {
                        enemy = enemy,
                        attackerData = this.GetModel<IPlayerModel>().components.Get<CharacterCombatData>()
                    });
                },
                canExit: state => state.timer.IsAnimatorFinish,
                needsExitTime: true
            );
            
            FSM.AddTransition<MoveState, ParryState>
                (transition => InputKit.Instance.parry);
            
            FSM.AddTransition<MoveState, JumpState>
                (transition => InputKit.Instance.jump);
			         
            FSM.AddTransition<MoveState, FallState>
                (transition => !sensorController.groundSensor);
            
            FSM.AddTransition<MoveState, StabState>
                (transition => sensorController.stabSensor && InputKit.Instance.attack);
            
            FSM.AddTransition<MoveState, BackStabState>
                (transition => sensorController.backStabSensor && InputKit.Instance.attack);
            
            FSM.AddTransition<MoveState, AttackState>
                (transition => InputKit.Instance.attack);
                
            FSM.AddTransition<MoveState, SpecialAbilityState>
                (transition => InputKit.Instance.fireball || InputKit.Instance.timeStop);
            
            FSM.AddTransition<JumpState, MoveState>
                (transition => sensorController.groundSensor);
            
            FSM.AddTransition<JumpState, FallState>
                (transition => true);
            
            FSM.AddTransition<FallState, LandState>
                (transition => Mathf.Abs(rb.velocity.y) > config.hardLandThreshold && sensorController.groundSensor);
            
            FSM.AddTransition<FallState, MoveState>
                (transition => sensorController.groundSensor);
            
            FSM.AddTransition<LandState, FallState>
                (transition => true);
            
            FSM.AddTransition<AttackState, AttackState>
            (transition => InputKit.Instance.attack,
                onTransition: transition => comboFlag = true);
            
            FSM.AddTransition<AttackState, MoveState>
                (transition => true);
            
            FSM.AddTransition<ParryState, MoveState>
                (transition => true);
            
            FSM.AddTransition<SpecialAbilityState, MoveState>
                (transition => !InputKit.Instance.fireball && !InputKit.Instance.timeStop);
            
            FSM.AddTransition<BackStabState, MoveState>
                (transition => true);
            
            FSM.AddTransition<StabState, MoveState>
                (transition => true);
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;

            cam = Camera.main;
            
            config = Resources.Load<PlayerConfig>("Data/Character/Player/Player Config");
            combatData = Resources.Load<CharacterCombatData>("Data/Character/Player/Player Combat Data");
            
            var model = this.GetModel<IPlayerModel>();
            model.RegisterPlayer(
                transform, 
                this, 
                animator, 
                rb, 
                sensorController,
                cam,
                combatData.Instantiate());
        }
        
        private void OnDisable()
        {
            var model = this.GetModel<IPlayerModel>();
            model.UnregisterPlayer();
        }

        private void Start()
        {
            FSM.Init();
        }

        private void Update()
        {
            FSM.OnLogic();
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

        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}
