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
        
        private float startTime;
        
        public override void OnStart()
        {
            startTime = Time.time;
        }

        public override Status OnUpdate()
        {
            if (startTime + waitTime < Time.time)
            {
                return Status.Success;
            }

            return Status.Running;
        }
    }
}