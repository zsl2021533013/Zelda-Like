using System;
using GraphProcessor;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Stabbed")]
    public class StabbedNode : ActionNode
    {
        private const string AnimationName = "Stabbed";

        private AnimationTimer timer;

        public override void OnEnable()
        {
            base.OnEnable();
            
            timer = new AnimationTimer(animator.GetAnimationLength(AnimationName));
        }

        public override void OnStart()
        {
            timer.Reset();
            
            animator.CrossFade(AnimationName, 0.1f);

            agent.updateRotation = false;
        }

        public override Status OnUpdate()
        {
            return timer.IsAnimatorFinish ? Status.Success : Status.Running;
        }
    }
}