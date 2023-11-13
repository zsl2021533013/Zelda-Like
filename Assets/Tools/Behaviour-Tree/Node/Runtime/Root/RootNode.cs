using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Behaviour_Tree.Node.Runtime.Root
{
    [Serializable]
    public class RootNode : BehaviourTreeNode
    {
        [Output("child", false)] public BehaviourTreeLink child;

        // public override Color color => Color.yellow;

        public override Status OnUpdate()
        {
            if (GetOutputNodes().FirstOrDefault() is BehaviourTreeNode childNode)
            {
                return childNode.Update();
            }
            else
            {
                return Status.Success;
            }
        }
    }
}