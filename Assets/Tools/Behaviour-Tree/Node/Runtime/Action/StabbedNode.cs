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
    [Serializable, NodeMenuItem("Behaviour/Action/Stabbed")]
    public class StabbedNode : EnemyActionNode
    {
        private string animationName = "Stabbed";

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
            
            var enemyStatus = this.GetModel<IEnemyModel>().GetComponents(transform).Get<EnemyStatus>();
            enemyStatus.isStabbed.Reset();
        }

        public override Status OnUpdate()
        {
            return timer.IsAnimatorFinish ? Status.Success : Status.Running;
        }
    }
}