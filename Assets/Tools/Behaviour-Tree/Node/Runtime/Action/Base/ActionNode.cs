using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Behaviour_Tree.Node.Runtime.Action
{
    public abstract class ActionNode : BehaviourTreeNode
    {
        [Input("parent", false)] public BehaviourTreeLink parent;
    }
}