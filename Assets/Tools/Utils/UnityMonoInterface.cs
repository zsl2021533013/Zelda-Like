using System;
using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.Behaviour_Tree.Utils
{
    public class UnityMonoInterface : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent onAnimatorMove = new UnityEvent();
        
        [HideInInspector]
        public UnityEvent onDrawGizmos = new UnityEvent();
        
        [HideInInspector]
        public UnityEvent onDrawGizmosSelected = new UnityEvent();

        private void OnAnimatorMove()
        {
            onAnimatorMove?.Invoke();
        }

        private void OnDrawGizmos()
        {
            onDrawGizmos?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            onDrawGizmosSelected?.Invoke();
        }
        
        public UnityMonoInterface RegisterAnimatorMove(UnityAction action)
        {
            onAnimatorMove.AddListener(action);
            return this;
        }

        public UnityMonoInterface RegisterDrawGizmos(UnityAction action)
        {
            onDrawGizmos.AddListener(action);
            return this;
        }
        
        public UnityMonoInterface RegisterDrawGizmosSelected(UnityAction action)
        {
            onDrawGizmosSelected.AddListener(action);
            return this;
        }
    }
}