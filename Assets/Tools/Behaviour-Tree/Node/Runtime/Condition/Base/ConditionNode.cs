using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition.Base
{
    public abstract class ConditionNode : EnemyBehaviourTreeNode
    {
        [Input("parent", false), Vertical, HideInInspector] public BehaviourTreeLink parent;
        
        public override string layoutStyle => "Behaviour Tree/ConditionNodeStyle";
    }
}