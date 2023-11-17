using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

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
            
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
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