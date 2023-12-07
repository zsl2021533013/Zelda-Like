using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition.Base
{
    public abstract class ConditionNode : BehaviourTreeNode
    {
        [Input("parent", false), Vertical] public BehaviourTreeLink parent;
        
        public override string layoutStyle => "Behaviour Tree/ConditionNodeStyle";
    }
}