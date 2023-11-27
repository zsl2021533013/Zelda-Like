using Controller.Combat;
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
        protected WeaponController weapon;
        protected EnemyConfig config;
        
        public override void OnEnable()
        {
            var components = this.GetModel<IEnemyModel>().GetComponents(transform);
            
            animator = components.Get<Animator>();
            agent = components.Get<NavMeshAgent>();
            weapon = components.Get<WeaponController>();
            config = components.Get<EnemyConfig>();
        }
    }
}