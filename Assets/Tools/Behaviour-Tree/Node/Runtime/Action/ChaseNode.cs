using System;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Chase")]
    public class ChaseNode : ActionNode
    {
        private const string AnimationName = "Chase";
        
        private Transform playerTrans;

        public override void OnStart()
        {
            animator.CrossFade(AnimationName, 0.1f);
            
            agent.updateRotation = true;
            
            playerTrans = this.GetModel<IPlayerModel>().components.Get<Transform>();
            
            EnemyWeapon.CloseWeapon();
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
                this.GetModel<IEnemyModel>().GetEnemyStatus(transform).state.Value = EnemyStatus.State.Safe;
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