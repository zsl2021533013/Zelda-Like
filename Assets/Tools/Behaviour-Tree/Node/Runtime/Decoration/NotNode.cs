using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using Behaviour_Tree.Node.Runtime.Decoration;
using GraphProcessor;

namespace Tools.Behaviour_Tree.Node.Runtime.Decoration
{
    [Serializable, NodeMenuItem("Behaviour/Decoration/Not")]
    public class NotNode : DecorationNode
    {
        public override Status OnUpdate()
        {
            var child = GetChildren().FirstOrDefault();

            if (child is null)
            {
                return Status.Success;
            }

            switch (child.Update())
            {
                case Status.Success:
                    return Status.Failure;
                case Status.Failure:
                    return Status.Success;
                case Status.Running:
                    return Status.Running;
                default:
                    return Status.Success;
            }
        }
    }
}