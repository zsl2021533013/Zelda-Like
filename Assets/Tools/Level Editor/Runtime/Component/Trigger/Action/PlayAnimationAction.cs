using QFramework.TimeExtend;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using UnityEngine;
using AnimationTimer = Tools.Behaviour_Tree.Utils.AnimationTimer;

namespace Level_Editor.Runtime.Action
{
    public class PlayAnimationAction : ActionBase
    {
        public Animator animator;

        public string animationName;

        private AnimationTimer timer;
        
        public override void OnEnter()
        {
            animator.Play(animationName);
            
            timer = new AnimationTimer(animator.GetAnimationLength(animationName));
        }

        public override bool CanExit()
        {
            return timer.IsAnimatorFinish;
        }
    }
}