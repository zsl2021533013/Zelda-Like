using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    [Serializable, NodeMenuItem("Behaviour/Condition/Detect Player")]
    public class DetectPlayerNode : ConditionNode
    {
        [ShowInInspector]
        public float radius;

        private Transform _transform;

        public override void OnAwake()
        {
            _transform = components.Get<Transform>();
        }

        public override Status OnUpdate()
        {
            var player = GetPlayer();
            
            return player ? Status.Success : Status.Failure;
        }

        private Collider GetPlayer()
        {
            var colliders = Physics.OverlapSphere(_transform.position, radius);

            return colliders.FirstOrDefault(collider => collider.CompareTag("Player"));
        }
    }
}