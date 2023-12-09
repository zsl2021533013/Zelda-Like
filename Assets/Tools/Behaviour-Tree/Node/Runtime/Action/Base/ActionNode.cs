using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Action.Base
{
    public abstract class ActionNode : EnemyBehaviourTreeNode
    {
        [Input("parent", false), Vertical, HideInInspector] public BehaviourTreeLink parent;

        public override string layoutStyle => "Behaviour Tree/ActionNodeStyle";
    }
}