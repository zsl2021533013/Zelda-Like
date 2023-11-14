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
        public StateMachine<Type, Type, Type> FSM { get; private set; }

        private void Awake()
        {
            FSM = new StateMachine<Type, Type, Type>();

            var groundFSM = new StateMachine<Type, Type, Type>();

            FSM.AddState(typeof(GroundSubFSM), groundFSM);

            // groundFSM.AddState<IdleState>(
            //     animator,
            //     "Idle",
            //     onEnter: state => { rb.velocity = Vector2.zero; });
            
            groundFSM.AddState<MoveState>(
                animator,
                "Ground",
                onLogic: state =>
                {
                    var movement = InputKit.Instance.move.Value.normalized;
                    var targetSpeed = movement.magnitude * 
                                      (InputKit.Instance.run ? config.runSpeed : config.walkSpeed);
                    var currentSpeed = animator.GetFloat(SpeedXZParam);
                    var nextSpeed = 0.05f.Lerp(currentSpeed, targetSpeed);
                    
                    animator.SetFloat(SpeedXZParam, nextSpeed);
                    
                    if (InputKit.Instance.move.Value.magnitude >= 0.1f)
                    {
                        var right = cam.transform.right;
                        var inputDirection = InputKit.Instance.move.Value.normalized;
                        var camForward = Vector3.Cross(right, Vector3.up);
                        var targetDir = right * inputDirection.x + camForward * inputDirection.y;
                        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.05f);
                    }
                });
            
            // groundFSM.AddTransition<MoveState, IdleState> 
            //     (transition => InputKit.Instance.move.Value.magnitude < 0.1f && rb.velocity.magnitude < 0.1f);
			         //
            // groundFSM.AddTransition<IdleState, MoveState> 
            //     (transition => InputKit.Instance.move.Value.magnitude >= 0.1f);
        }

        private void Start()
        {
            FSM.Init();
        }

        private void Update()
        {
            FSM.OnLogic();
        }
    }
}
