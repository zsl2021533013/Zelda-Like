using System;
using GraphProcessor;
using Script.View_Controller.Character_System.HFSM.Util;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Idle")]
    public class IdleNode : ActionNode
    {
        private const string AnimationName = "Idle";
        
        private AnimationTimer timer;

        public override void OnEnable()
        {
            base.OnEnable();

            timer = new AnimationTimer();
        }

        public override void OnStart()
        {
            base.OnStart();
            
            timer.Reset();

            if (animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName))
            {
                return;
            }
            
            animator.CrossFade(AnimationName, 0.1f);
            
            EnemyWeapon.CloseWeapon();
        }

        public override Status OnUpdate()
        {
            agent.SetDestination(transform.position);

            if (timer > 0.2f) // 需要等待动画转态完成，否则不断的请求会导致卡住
            {
                return Status.Success;
            }
            
            return Status.Running;
        }
    }
}