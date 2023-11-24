using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Search")]
    public class SearchNode : EnemyActionNode
    {
        private float angle;
        private float distance;
        private Transform playerTrans;
        private Vector3 targetPos;

        public override void OnAwake()
        {
            base.OnAwake();
            
            angle = config.povAngle;
            distance = config.povDist;
        }

        public override void OnStart()
        {
            animator.CrossFade("Chase", 0.1f);
            
            agent.updateRotation = true;
            
            playerTrans = this.GetModel<IPlayerModel>().transform;
            targetPos = playerTrans.position;
            
            agent.SetDestination(targetPos);
        }

        public override Status OnUpdate()
        {
            if (Vector3.Distance(transform.position, targetPos) <= 1f || 
                IsPlayerInSectorRange(playerTrans.position))
            {
                return Status.Success;
            }

            return Status.Running;
        }

        public override void OnStop()
        {
            agent.updateRotation = false;
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