﻿using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Level_Editor.Runtime.Event
{
    public interface IEvent
    {
        public IEnumerable<UnityEvent> callbacks { get; }
        
        public IEnumerable<Transform> connections { get; }
    }
    
    [Serializable]
    public abstract class EventBase : IEvent
    {
        public abstract IEnumerable<UnityEvent> callbacks { get; }
        
        public virtual IEnumerable<Transform> connections { get; }

        public void Register(TriggerController controller)
        {
            callbacks.ForEach(callback => callback?.AddListener(controller.TryTrigger));
        }

        public void Unregister(TriggerController controller)
        {
            callbacks.ForEach(callback => callback?.RemoveListener(controller.TryTrigger));
        }
    }
}