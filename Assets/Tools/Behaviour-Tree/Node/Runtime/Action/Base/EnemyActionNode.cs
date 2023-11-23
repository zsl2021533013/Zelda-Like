using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour_Tree.Node.Runtime.Action
{
    public abstract class EnemyActionNode : ActionNode
    {
        protected Animator animator;
        protected NavMeshAgent agent;
        protected EnemyConfig config;
        
        public override void OnAwake()
        {
            var components = this.GetModel<IEnemyModel>().GetComponents(transform);
            
            animator = components.Get<Animator>();
            agent = components.Get<NavMeshAgent>();
            config = components.Get<EnemyConfig>();
        }
    }
}