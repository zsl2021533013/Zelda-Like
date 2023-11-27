using System;
using Controller.Combat;
using QFramework;
using UnityEngine;

namespace Controller.Environment
{
    public class FireballController : MonoBehaviour, IController, IParried, IStopped
    {
        private void OnEnable()
        {
            
        }

        public void Parried()
        {
            
        }

        public void Stopped()
        {
            
        }
        
        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}