using System.Linq;
using Model.Interface;
using QFramework;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class MovePlatformAction : ActionBase
    {
        public Transform platformArea;
        
        public Animator animator;

        public string animationName;

        private AnimationTimer timer;

        private Transform playerTrans;

        public override void OnEnter()
        {
            animator.Play(animationName);
            
            timer = new AnimationTimer(animator.GetAnimationLength(animationName));

            playerTrans = this.GetModel<IPlayerModel>().transform;
            
            DetectPlayer();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            DetectPlayer();
        }

        public override void OnExit()
        {
            playerTrans.SetParent(null);
        }

        public override bool CanExit()
        {
            return timer.IsAnimatorFinish;
        }

        private void DetectPlayer()
        {
            var colliders = Physics.OverlapBox(platformArea.position, platformArea.localScale / 2f);
            var player = colliders.FirstOrDefault(collider => collider.CompareTag("Player"));

            playerTrans.SetParent(player ? animator.transform : null);
        }
    }
}