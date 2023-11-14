using System;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    public class CameraPointController : MonoBehaviour
    {
        private Transform _follow;
        private Vector3 _offset;

        private void Awake()
        {
            _follow = transform.parent;
            _offset = transform.localPosition;

            transform.parent = null;
        }

        private void FixedUpdate()
        {
            transform.position = _follow.position + _offset;
        }
    }
}