using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    public abstract class ConditionNode : BehaviourTreeNode
    {
        [Input("parent", false)] public BehaviourTreeLink parent;
    }
}