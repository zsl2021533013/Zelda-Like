using System;
using Sirenix.OdinInspector;

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
        public virtual void OnEnable() { }

        public abstract bool Satisfied();
    }
}