using System;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Composite.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Composite
{
    [Serializable, NodeMenuItem("Behaviour/Composite/Sequence")]
    public class SequenceNode : CompositeNode
    {
        private int index;
        
        public override void OnStart()
        {
            index = 0;
        }

        public override Status OnUpdate()
        {
            var childNode = children[index];

            switch (childNode.Update())
            {
                case Status.Success:
                    index++;
                    break;
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    return Status.Failure;
            }

            return index == children.Count ? Status.Success : Status.Running;
        }
    }
}