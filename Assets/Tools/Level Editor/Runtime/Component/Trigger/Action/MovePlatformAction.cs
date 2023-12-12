using System.Linq;
using DG.Tweening;
using Model.Interface;
using QFramework;
using Script.View_Controller.Character_System.HFSM.StateMachine;
using Script.View_Controller.Character_System.HFSM.Util;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class MovePlatformAction : ActionBase
    {
        private const float Speed = 4f;
        
        public Transform platform;
        public Transform destination;

        private bool isCompleted;
        
        public override void OnEnter()
        {
            platform.DOKill();
            
            var platformPos = platform.position;
            var destinationPos = destination.position;
            
            isCompleted = false;

            if (Vector3.Distance(platformPos, destinationPos) < 0.1f)
            {
                isCompleted = true;
                return;
            }

            var duration = Vector3.Distance(platformPos, destinationPos) / Speed;
            platform.DOMove(destinationPos, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => isCompleted = true);
        }

        public override bool CanExit()
        {
            return isCompleted;
        }
    }
}