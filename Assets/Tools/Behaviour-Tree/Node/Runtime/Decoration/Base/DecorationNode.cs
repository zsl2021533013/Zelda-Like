using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Behaviour_Tree.Node.Runtime.Decoration
{
    public abstract class DecorationNode : BehaviourTreeNode
    {
        [Input("parent", false), Vertical] public BehaviourTreeLink parent;
        [Output("child", false), Vertical] public BehaviourTreeLink child;
        
        public override string layoutStyle => "DecorationNodeStyle";
    }
}