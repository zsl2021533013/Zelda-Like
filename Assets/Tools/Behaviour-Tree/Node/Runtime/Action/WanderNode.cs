using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Wander")]
    public class WanderNode : ActionNode
    {
        [ShowInInspector]
        public string animationName;
        
        private Transform _transform;
        private Transform _playerTrans;
        private Animator _animator;
        private NavMeshAgent _agent;

        private AnimationTimer _timer;

        public override void OnAwake()
        {
            _transform = components.Get<Transform>();
            _animator = components.Get<Animator>();
            _agent = components.Get<NavMeshAgent>();
            
            _timer = new AnimationTimer(_animator.GetAnimationLength(animationName));
        }

        public override void OnStart()
        {
            _timer.Reset();
            
            _animator.CrossFade(animationName, 0.1f);
            
            _agent.updateRotation = true;
        }

        public override Status OnUpdate()
        {
            _playerTrans = GetPlayer()?.transform;
            _agent.SetDestination(_playerTrans.position);
            
            return _timer.IsAnimatorFinish ? Status.Success : Status.Running;
        }

        public override void OnStop()
        {
            _agent.updateRotation = false;
        }
        
        private Collider GetPlayer()
        {
            var colliders = Physics.OverlapSphere(_transform.position, 100);

            return colliders.FirstOrDefault(collider => collider.CompareTag("Player"));
        }
    }
}