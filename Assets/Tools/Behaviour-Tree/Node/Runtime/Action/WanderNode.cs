using System;
using GraphProcessor;
using Model.Interface;
using QFramework;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Wander")]
    public class WanderNode : ActionNode
    {
        private const string animationName = "Wander";
        
        private Transform playerTrans;

        private AnimationTimer timer;

        public override void OnEnable()
        {
            base.OnEnable();
            
            timer = new AnimationTimer(animator.GetAnimationLength(animationName));
        }

        public override void OnStart()
        {
            timer.Reset();
            
            animator.CrossFade(animationName, 0.1f);
            
            agent.updateRotation = true;
            
            playerTrans = this.GetModel<IPlayerModel>().components.Get<Transform>();
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