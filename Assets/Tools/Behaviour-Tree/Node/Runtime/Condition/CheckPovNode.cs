using System;
using GraphProcessor;
using Model.Interface;
using QFramework;
using Tools.Behaviour_Tree.Node.Runtime.Condition.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    [Serializable, NodeMenuItem("Behaviour/Condition/Check Pov")]
    public class CheckPovNode : ConditionNode
    {
        private float angle;
        private float distance;
        private Transform playerTrans;

        public override void OnEnable()
        {
            base.OnEnable();

            angle = config.povAngle;
            distance = config.povDist;
        }

        public override void OnStart()
        {
            playerTrans = this.GetModel<IPlayerModel>().components.Get<Transform>();
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
            var origin = transform.position + Vector3.up;
            var direction = tarPos - origin;
            var deltaAngle = Vector3.Angle(transform.forward, direction);
            
            if (deltaAngle < angle)
            {
                var dist = Vector3.Distance(origin, tarPos);
                if (dist < distance)
                {
                    // 判断有无遮挡
                    var ray = new Ray(origin, direction);
                    if (Physics.Raycast(ray, out var info, Vector3.Distance(tarPos, origin)))
                    {
                        if (info.collider.CompareTag("Player"))
                        {
                            return true;
                        }
                    }
                }
            }
            
            return false;
        }
    }
}