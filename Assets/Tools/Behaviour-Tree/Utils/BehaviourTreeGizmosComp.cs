using System;
using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.Behaviour_Tree.Utils
{
    public class BehaviourTreeGizmosComp : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent onDrawGizmos = new UnityEvent();
        
        [HideInInspector]
        public UnityEvent onDrawGizmosSelected = new UnityEvent();

        private void OnDrawGizmos()
        {
            onDrawGizmos?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            onDrawGizmosSelected?.Invoke();
        }

        public BehaviourTreeGizmosComp RegisterDrawGizmos(UnityAction action)
        {
            onDrawGizmos.AddListener(action);
            return this;
        }
        
        public BehaviourTreeGizmosComp RegisterDrawGizmosSelected(UnityAction action)
        {
            onDrawGizmosSelected.AddListener(action);
            return this;
        }
    }
}