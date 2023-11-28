using System;
using Behaviour_Tree.Node.Runtime.Core;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using UnityEngine;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Back Stabbed")]
    public class BackStabbedNode : EnemyActionNode
    {
        private string animationName = "Back Stabbed";

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
        }

        public override Status OnUpdate()
        {
            return timer.IsAnimatorFinish ? Status.Success : Status.Running;
        }
    }
}