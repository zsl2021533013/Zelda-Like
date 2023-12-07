using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Decoration.Base
{
    public abstract class DecorationNode : BehaviourTreeNode
    {
        [Input("parent", false), Vertical] public BehaviourTreeLink parent;
        [Output("child", false), Vertical] public BehaviourTreeLink child;
        
        public override string layoutStyle => "Behaviour Tree/DecorationNodeStyle";
    }
}