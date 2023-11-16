using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Chase")]
    public class ChaseNode : ActionNode
    {
        [ShowInInspector]
        public float attackRadius;
        
        [ShowInInspector]
        public float chaseRadius;
        
        private Transform _transform;
        private Transform _playerTrans;
        private Animator _animator;
        private NavMeshAgent _agent;

        public override void OnAwake()
        {
            _transform = components.Get<Transform>();
            _animator = components.Get<Animator>();
            _agent = components.Get<NavMeshAgent>();
        }

        public override void OnStart()
        {
            _animator.CrossFade("Chase", 0.1f);
            
            _agent.updateRotation = true;
        }

        public override Status OnUpdate()
        {
            _playerTrans = GetPlayer()?.transform;
            
            if (_playerTrans)
            {
                _agent.SetDestination(_playerTrans.position);
                
                if (Vector3.Distance(_transform.position, _playerTrans.position) < attackRadius)
                {
                    return Status.Success;
                }

                return Status.Running;
            }

            return Status.Failure;
        }

        public override void OnStop()
        {
            _agent.updateRotation = false;
        }

        private Collider GetPlayer()
        {
            var colliders = Physics.OverlapSphere(_transform.position, chaseRadius);

            return colliders.FirstOrDefault(collider => collider.CompareTag("Player"));
        }
    }
}