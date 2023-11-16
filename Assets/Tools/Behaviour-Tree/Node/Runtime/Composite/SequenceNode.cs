using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Behaviour_Tree.Node.Runtime.Composite
{
    [Serializable, NodeMenuItem("Behaviour/Composite/Sequence")]
    public class SequenceNode : CompositeNode
    {
        private int _index;
        
        public override void OnStart()
        {
            _index = 0;
        }

        public override Status OnUpdate()
        {
            var children = GetChildren();
            var childNode = children[_index];

            switch (childNode.Update())
            {
                case Status.Success:
                    _index++;
                    break;
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    return Status.Failure;
            }

            return _index == children.Count ? Status.Success : Status.Running;
        }
    }
}