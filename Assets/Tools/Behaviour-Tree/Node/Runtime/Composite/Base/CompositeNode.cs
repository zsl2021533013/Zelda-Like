using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Behaviour_Tree.Node.Runtime.Composite
{
    public abstract class CompositeNode : BehaviourTreeNode
    {
        [Input("parent", false)] public BehaviourTreeLink parent;
        [Output("child", true)] public BehaviourTreeLink child;
    }
}