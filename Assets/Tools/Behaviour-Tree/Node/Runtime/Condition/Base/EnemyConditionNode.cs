using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition.Base
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