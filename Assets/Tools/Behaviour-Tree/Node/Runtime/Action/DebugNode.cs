using System;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
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