using Cinemachine;
using Controller.Character.Player.Player;
using Script.View_Controller.Character_System.HFSM.Util;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class ChangeCameraAction : ActionBase
    {
        public CinemachineVirtualCameraBase camera;
        public float duration;

        private Timer timer;
        
        public override void OnEnter()
        {
            camera.Priority = 1000;

            timer = new Timer();

            var cameraController = Object.FindObjectOfType<CameraController>();
            cameraController.enableCameraPoint = false;
        }

        public override void OnUpdate()
        {
            if (timer > duration)
            {
                camera.Priority = 0;
            }
        }

        public override void OnExit()
        {
            var cameraController = Object.FindObjectOfType<CameraController>();
            cameraController.enableCameraPoint = true;
        }

        public override bool CanExit()
        {
            return timer > (duration + 2f); // 我们有两秒的混合时间，故加二
        }
    }
}