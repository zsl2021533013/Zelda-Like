using System.Collections.Generic;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using Tools.Behaviour_Tree.Utils;

namespace Tools.Behaviour_Tree.Node.Runtime.Composite.Base
{
    public abstract class CompositeNode : BehaviourTreeNode
    {
        [Input("parent", false), Vertical] public BehaviourTreeLink parent;
        [Output("child", true), Vertical] public BehaviourTreeLink child;
        
        public override string layoutStyle => "Behaviour Tree/CompositeNodeStyle";

        public override void OnEnable()
        {
            children = this.GetChildren();
        }

        protected List<BehaviourTreeNode> children = new List<BehaviourTreeNode>();
    }
}