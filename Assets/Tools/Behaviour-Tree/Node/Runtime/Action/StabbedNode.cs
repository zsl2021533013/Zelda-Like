using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Stabbed")]
    public class StabbedNode : EnemyActionNode
    {
        private string animationName = "Stabbed";

        private AnimationTimer timer;

        public override void OnEnable()
        {
            base.OnEnable();
            
            timer = new AnimationTimer(animator.GetAnimationLength(animationName));
        }

        public override void OnStart()
        {
            timer.Reset();
            
            animator.CrossFade(animationName, 0.1f);
        }

        public override Status OnUpdate()
        {
            return timer.IsAnimatorFinish ? Status.Success : Status.Running;
        }
    }
}