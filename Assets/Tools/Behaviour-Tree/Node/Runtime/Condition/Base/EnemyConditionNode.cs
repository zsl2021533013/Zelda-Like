using Data.Character.Enemy;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    public abstract class EnemyConditionNode : ConditionNode
    {
        protected Transform transform;
        protected Animator animator;
        protected NavMeshAgent agent;
        protected EnemyConfig config;
        
        public override void OnAwake()
        {
            transform = components.Get<Transform>();
            animator = components.Get<Animator>();
            agent = components.Get<NavMeshAgent>();
            config = components.Get<EnemyConfig>();
        }
    }
}