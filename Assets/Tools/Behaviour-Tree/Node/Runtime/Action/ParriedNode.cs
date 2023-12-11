using System;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Parried")]
    public class ParriedNode : ActionNode
    {
        private const string AnimationName = "Parried";

        private AnimationTimer timer;

        public override void OnEnable()
        {
            base.OnEnable();
            
            timer = new AnimationTimer(animator.GetAnimationLength(AnimationName));
        }

        public override void OnStart()
        {
            timer.Reset();
            
            animator.CrossFade(AnimationName, 0.1f);
        }

        public override Status OnUpdate()
        {
            return timer.IsAnimatorFinish ? Status.Success : Status.Running;
        }

        public override void OnStop()
        {
            var enemyStatus = this.GetModel<IEnemyModel>().GetComponents(transform).Get<EnemyStatus>();
            enemyStatus.isParried.ResetWithoutEvent();
        }
    }
}