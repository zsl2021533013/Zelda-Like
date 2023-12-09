using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level_Editor.Runtime
{
    public enum ConditionType
    {
        PlayerEnter,
        TriggerComplete
    }
    
    public interface ICondition
    {
        public void OnEnable();
        public bool Satisfied();
    }
    
    [HideLabel]
    [InlineProperty]
    [Serializable]
    public abstract class ConditionBase : ICondition
    {
        [HideInInspector]
        public TriggerController controller;
        
        public virtual void OnEnable() { }
        
        public virtual void OnDisable() { }

        public abstract bool Satisfied();
    }
}