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
        public bool Satisfied();
    }
    
    [HideLabel]
    [InlineProperty]
    [Serializable]
    public abstract class ConditionBase : ICondition
    {
        public abstract bool Satisfied();
    }
}