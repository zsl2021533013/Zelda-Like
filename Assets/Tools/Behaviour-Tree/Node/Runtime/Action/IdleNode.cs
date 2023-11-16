using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Idle")]
    public class IdleNode : ActionNode
    {
        private Transform _transform;
        private Animator _animator;
        private NavMeshAgent _agent;

        public override void OnAwake()
        {
            _transform = components.Get<Transform>();
            _animator = components.Get<Animator>();
            _agent = components.Get<NavMeshAgent>();
        }
        
        public override Status OnUpdate()
        {
            _agent.SetDestination(_transform.position);
            
            _animator.CrossFade("Idle", 0.1f);

            return Status.Success;
        }
    }
}