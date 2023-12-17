using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.Level_Editor.Runtime.Component.Trigger
{
    public class TriggerInterface : MonoBehaviour
    {
        public bool detectTag;
        [ShowIf("detectTag", true)] public string collisionTag;
        
        public bool detectLayer;
        [ShowIf("detectLayer", true)] public LayerMask collisionLayer;

        [HideInInspector] public UnityEvent onTriggerEnter = new UnityEvent();
        [HideInInspector] public UnityEvent onTriggerStay = new UnityEvent();
        [HideInInspector] public UnityEvent onTriggerExit = new UnityEvent();
        
        private List<Collider> previousColliders = new List<Collider>();
        
        private void Update()
        {
            // 模拟 OnTriggerEnter
            CheckTriggers();
        }

        private void CheckTriggers()
        {
            var colliders = Physics.OverlapBox(transform.position, 
                transform.localScale / 2f, 
                transform.rotation, 
                 detectLayer ? collisionLayer : -1).ToList();

            if (detectTag)
            {
                colliders = colliders.Where(collider => collider.CompareTag(collisionTag)).ToList();
            }

            foreach (var collider in colliders)
            {
                // 检测是否是新进入的物体
                if (!previousColliders.Contains(collider))
                {
                    // 模拟 OnTriggerEnter
                    OnTriggerEnterSimulation(collider);
                }
                else
                {
                    // 模拟 OnTriggerStay
                    OnTriggerStaySimulation(collider);
                }
            }

            // 检测是否有离开的物体
            foreach (var previousCollider in previousColliders)
            {
                if (!colliders.Contains(previousCollider))
                {
                    // 模拟 OnTriggerExit
                    OnTriggerExitSimulation(previousCollider);
                }
            }

            // 更新上一帧的碰撞器列表
            previousColliders = colliders;
        }

        private void OnTriggerEnterSimulation(Collider other)
        {
            // 模拟 OnTriggerEnter 逻辑
            onTriggerEnter?.Invoke();
        }

        private void OnTriggerStaySimulation(Collider other)
        {
            // 模拟 OnTriggerStay 逻辑
            onTriggerStay?.Invoke();
        }

        private void OnTriggerExitSimulation(Collider other)
        {
            // 模拟 OnTriggerExit 逻辑
            onTriggerExit?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}
