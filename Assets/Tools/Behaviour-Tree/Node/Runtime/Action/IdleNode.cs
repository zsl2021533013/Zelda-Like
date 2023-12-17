using System;
using GraphProcessor;
using Script.View_Controller.Character_System.HFSM.Util;
using Sirenix.OdinInspector;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Idle")]
    public class IdleNode : ActionNode
    {
        [GraphProcessor.ShowInInspector]
        public bool needExitTime;

        [GraphProcessor.ShowInInspector, ShowIf("needExitTime", true)]
        public float exitTime;
        
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

            if (needExitTime) // 需要等待动画转态完成，否则不断的请求会导致卡住
            {
                return timer > exitTime ? Status.Success : Status.Running;
            }
            else
            {
                return timer > 0.2f ? Status.Success : Status.Running;
            }
        }
    }
}