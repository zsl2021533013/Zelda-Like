using System.Collections.Generic;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;

namespace Behaviour_Tree.Node.Runtime.Composite
{
    public abstract class CompositeNode : BehaviourTreeNode
    {
        [Input("parent", false), Vertical] public BehaviourTreeLink parent;
        [Output("child", true), Vertical] public BehaviourTreeLink child;
        
        public override string layoutStyle => "CompositeNodeStyle";

        public override void OnAwake()
        {
            children = this.GetChildren();
        }

        protected List<BehaviourTreeNode> children = new List<BehaviourTreeNode>();
    }
}