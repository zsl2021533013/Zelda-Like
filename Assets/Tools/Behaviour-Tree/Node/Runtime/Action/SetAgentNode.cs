using System;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Set Agent")]
    public class SetAgentNode : ActionNode
    {
        [ShowInInspector]
        public bool isRotationUpdate;

        public override void OnStart()
        {
            agent.updateRotation = isRotationUpdate;
        }

        public override Status OnUpdate()
        {
            return Status.Success;
        }
    }
}