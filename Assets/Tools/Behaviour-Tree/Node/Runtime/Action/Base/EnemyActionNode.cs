using Behaviour_Tree.Node.Runtime.Core;
using Data.Character.Enemy;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour_Tree.Node.Runtime.Action
{
    public abstract class EnemyActionNode : ActionNode
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