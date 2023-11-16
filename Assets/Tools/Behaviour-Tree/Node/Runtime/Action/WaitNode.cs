using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using UnityEngine;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Wait")]
    public class WaitNode : ActionNode
    {
        [ShowInInspector] public float waitTime;
        
        private float _startTime;
        
        public override void OnStart()
        {
            _startTime = Time.time;
        }

        public override Status OnUpdate()
        {
            if (_startTime + waitTime < Time.time)
            {
                return Status.Success;
            }

            return Status.Running;
        }
    }
}