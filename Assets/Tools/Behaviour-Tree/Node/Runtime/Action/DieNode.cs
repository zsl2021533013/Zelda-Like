using System;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Die")]
    public class DieNode : ActionNode
    {
        private const string AnimationName = "Die";
        
        public override void OnStart()
        {
            animator.CrossFade(AnimationName, 0.1f);
        }

        public override Status OnUpdate()
        {
            return Status.Success;
        }
    }
}