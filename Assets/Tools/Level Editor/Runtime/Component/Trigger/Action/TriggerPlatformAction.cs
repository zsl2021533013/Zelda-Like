using System.Linq;
using DG.Tweening;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class TriggerPlatformAction : ActionBase
    {
        private const float Speed = 4f;

        public Transform platformArea;
        public Transform top;
        public Transform bottom;

        private Transform player;
        private bool isCompleted;
        
        public override void OnEnter()
        {
            controller.transform.DOKill();

            isCompleted = false;

            var topPos = top.position;
            var bottomPos = bottom.position;
            var position = controller.transform.position;
            if (Vector3.Distance(position, topPos) < 0.1f)
            {
                controller.transform.DOMove(bottomPos, Vector3.Distance(bottomPos, position) / Speed)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => isCompleted = true);
            }
            else
            {
                controller.transform.DOMove(topPos, Vector3.Distance(topPos, position) / Speed)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => isCompleted = true);
            }

            player = this.GetModel<IPlayerModel>().components.Get<Transform>();
        }

        public override void OnUpdate()
        {
            var colliders =
                Physics.OverlapBox(platformArea.position, platformArea.localScale / 2f, platformArea.rotation);
            var detectPlayer = colliders.FirstOrDefault(collider => collider.CompareTag("Player"));
            
            player.SetParent(detectPlayer ? controller.transform : null);
        }

        public override void OnExit()
        {
            player.SetParent(null);
        }

        public override bool CanExit()
        {
            return isCompleted;
        }
    }
}