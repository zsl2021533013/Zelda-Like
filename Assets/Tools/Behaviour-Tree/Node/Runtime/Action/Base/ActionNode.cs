using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Action.Base
{
    public abstract class ActionNode : BehaviourTreeNode
    {
        [Input("parent", false), Vertical] public BehaviourTreeLink parent;

        public override string layoutStyle => "Behaviour Tree/ActionNodeStyle";
    }
}