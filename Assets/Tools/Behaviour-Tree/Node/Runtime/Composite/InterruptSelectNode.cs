using System;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Composite
{
    [Serializable, NodeMenuItem("Behaviour/Composite/Interrupt Select")]
    public class InterruptSelectNode : SelectNode
    {
        public override Status OnUpdate()
        {
            for (var i = 0; i < index; i++)
            {
                var childStatus = children[i].Update();
                if (childStatus != Status.Failure)
                {
                    children[index].Abort();
                    index = i;
                    break;
                }
            }
            
            return base.OnUpdate();
        }
    }
}