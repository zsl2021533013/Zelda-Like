using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    public abstract class EnemyConditionNode : ConditionNode
    {
        protected Animator animator;
        protected NavMeshAgent agent;
        protected EnemyConfig config;
        
        public override void OnEnable()
        {
            var components = this.GetModel<IEnemyModel>().GetComponents(transform);
            animator = components.Get<Animator>();
            agent = components.Get<NavMeshAgent>();
            config = components.Get<EnemyConfig>();
        }
    }
}