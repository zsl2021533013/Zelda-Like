using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Debug")]
    public class DebugNode : ActionNode
    {
        [ShowInInspector]
        public string text;
        
        public override Status OnUpdate()
        {
            Debug.Log(text);
            return Status.Success;
        }
    }
}