using Controller.Combat;
using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Tools.Behaviour_Tree.Node.Runtime.Core
{
    public class EnemyBehaviourTreeNode : BehaviourTreeNode
    {
        protected Animator animator;
        protected NavMeshAgent agent;
        protected EnemyWeaponController EnemyWeapon;
        protected EnemyConfig config;
        protected EnemyStatus enemyStatus;
        
        public override void OnEnable()
        {
            var components = this.GetModel<IEnemyModel>().GetComponents(transform);
            
            animator = components.Get<Animator>();
            agent = components.Get<NavMeshAgent>();
            EnemyWeapon = components.Get<EnemyWeaponController>();
            config = components.Get<EnemyConfig>();
            enemyStatus = components.Get<EnemyStatus>();
        }

        public override Status OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}