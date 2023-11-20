using System;
using Script.View_Controller.Input_System;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    public class CameraController : MonoBehaviour
    {
        public CameraConfig config;

        public Transform cameraPoint;

        public bool enableCameraPoint = true;
        
        private Transform _follow;
        private Vector3 _offset;

        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        private void Awake()
        {
            _follow = transform.parent;
            _offset = cameraPoint.position - transform.parent.position;

            transform.parent = null;
        }

        private void Update()
        {
            cameraPoint.position = _follow.position + _offset;
        }

        private void LateUpdate()
        {
            UpdateCameraPoint();
        }

        private void UpdateCameraPoint()
        {
            if (!enableCameraPoint)
            {
                return;
            }
            
            var rotate = InputKit.Instance.rotate.Value;
            // if there is an input and camera position is not fixed
            if (rotate.sqrMagnitude >= 0.01f)
            {
                _cinemachineTargetYaw += rotate.x * config.cameraSpeedX * Time.deltaTime;
                _cinemachineTargetPitch += -rotate.y * config.cameraSpeedY * Time.deltaTime;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, config.bottomClamp, config.topClamp);

            // Cinemachine will follow this target
            var targetRot = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
            var currentRot = cameraPoint.transform.rotation;

            cameraPoint.transform.rotation = Quaternion.Lerp(currentRot, targetRot, 0.1f);
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}