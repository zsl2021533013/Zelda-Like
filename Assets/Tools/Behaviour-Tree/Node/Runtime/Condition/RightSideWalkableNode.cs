using System;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Condition.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    [Serializable, NodeMenuItem("Behaviour/Condition/Right Side Walkable")]
    public class RightSideWalkableNode : ConditionNode
    {
        public override Status OnUpdate()
        {
            var leftSide = transform.position - 2 * transform.right;

            // 进行射线检测
            var path = new NavMeshPath();
            agent.CalculatePath(leftSide, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                return Status.Success;
            }
            else
            {
                return Status.Failure;
            }
        }
    }
}