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
        [Input("text", false)] public string text;
        
        public override string name => "Debug";
        
        public override Status OnUpdate()
        {
            Debug.Log(text);
            return Status.Success;
        }
    }
}