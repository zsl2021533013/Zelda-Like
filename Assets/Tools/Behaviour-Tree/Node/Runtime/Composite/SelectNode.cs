using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;

namespace Behaviour_Tree.Node.Runtime.Composite
{
    [Serializable, NodeMenuItem("Behaviour/Composite/Select")]
    public class SelectNode: CompositeNode
    {
        protected int index;
        
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
                    return Status.Success;
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    index++;
                    break;
            }

            return index == children.Count ? Status.Failure : Status.Running;
        }
    }
}