using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Wander")]
    public class WanderNode : EnemyActionNode
    {
        [ShowInInspector]
        public string animationName;
        
        private Transform playerTrans;

        private AnimationTimer timer;

        public override void OnAwake()
        {
            base.OnAwake();
            
            timer = new AnimationTimer(animator.GetAnimationLength(animationName));
        }

        public override void OnStart()
        {
            timer.Reset();
            
            animator.CrossFade(animationName, 0.1f);
            
            agent.updateRotation = true;
            
            playerTrans = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        public override Status OnUpdate()
        {
            agent.SetDestination(playerTrans.position);
            
            return timer.IsAnimatorFinish ? Status.Success : Status.Running;
        }

        public override void OnStop()
        {
            agent.updateRotation = false;
        }
    }
}