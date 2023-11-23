using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Behaviour_Tree.Node.Runtime.Root
{
    [Serializable]
    public class RootNode : BehaviourTreeNode
    {
        [Output("child", false), Vertical] public BehaviourTreeLink child;

        public override string layoutStyle => "RootNodeStyle";

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