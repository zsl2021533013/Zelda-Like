using System;
using System.Linq;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Root
{
    [Serializable]
    public class RootNode : BehaviourTreeNode
    {
        [Output("child", false), Vertical] public BehaviourTreeLink child;

        public override string layoutStyle => "Behaviour Tree/RootNodeStyle";

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