using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public interface IAction
    {
        public void Perform(TriggerController controller);
    }
    
    [Serializable]
    public abstract class ActionBase : IAction
    {
        public abstract void Perform(TriggerController controller);
    }
}