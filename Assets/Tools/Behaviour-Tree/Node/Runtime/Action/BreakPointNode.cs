using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using UnityEngine;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Break Point")]
    public class BreakPointNode : EnemyActionNode
    {
        public override void OnStart() {
            Debug.Log("Breakpoint");
            Debug.Break();
        }

        public override Status OnUpdate() {
            return Status.Success;
        }
    }
}