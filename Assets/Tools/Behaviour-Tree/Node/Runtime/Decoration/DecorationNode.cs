using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Behaviour_Tree.Node.Runtime.Decoration
{
    public abstract class DecorationNode : BehaviourTreeNode
    {
        [Input("parent", false)] public BehaviourTreeLink parent;
        [Output("child", false)] public BehaviourTreeLink child;
    }
}