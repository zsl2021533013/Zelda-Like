using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    [Serializable, NodeMenuItem("Behaviour/Condition/Check Player In Pov")]
    public class CheckPlayerInPovNode : EnemyConditionNode
    {
        private float angle;
        private float distance;
        private Transform playerTrans;

        public override void OnAwake()
        {
            base.OnAwake();

            angle = config.povAngle;
            distance = config.povDist;
        }

        public override void OnStart()
        {
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override Status OnUpdate()
        {
            if (IsPlayerInSectorRange(playerTrans.position))
            {
                return Status.Success;
            }

            return Status.Failure;
        }

        private bool IsPlayerInSectorRange(Vector3 tarPos)
        {
            var toTarVec = tarPos - transform.position;
            var deltaAngle = Vector3.Angle(transform.forward, toTarVec);
            
            if (deltaAngle < angle)
            {
                var dist = Vector3.Distance(transform.position, tarPos);
                if (dist < distance)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}