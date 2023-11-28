using System;
using Behaviour_Tree.Node.Runtime.Core;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Chase")]
    public class ChaseNode : EnemyActionNode
    {
        private Transform playerTrans;

        public override void OnStart()
        {
            animator.CrossFade("Chase", 0.1f);
            
            agent.updateRotation = true;
            
            playerTrans = this.GetModel<IPlayerModel>().components.Get<Transform>();
        }

        public override Status OnUpdate()
        {
            agent.SetDestination(playerTrans.position);
                
            if (Vector3.Distance(transform.position, playerTrans.position) <= config.attackDist - 0.1f)
            {
                return Status.Success;
            }
            
            if (Vector3.Distance(transform.position, playerTrans.position) > config.povDist)
            {
                this.GetModel<IEnemyModel>().GetEnemyStatus(transform).state.Set(EnemyState.Safe);
                return Status.Failure;
            }

            return Status.Running;
        }

        public override void OnStop()
        {
            agent.updateRotation = false;
        }
    }
}