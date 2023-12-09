using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Decoration.Base
{
    public abstract class DecorationNode : EnemyBehaviourTreeNode
    {
        [Input("parent", false), Vertical, HideInInspector] public BehaviourTreeLink parent;
        [Output("child", false), Vertical, HideInInspector] public BehaviourTreeLink child;
        
        public override string layoutStyle => "Behaviour Tree/DecorationNodeStyle";
    }
}