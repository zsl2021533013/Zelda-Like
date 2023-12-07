using System.Collections.Generic;
using Controller.Character.Player.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Level_Editor.Runtime.Event
{
    public class TriggerUpdateEvent : EventBase
    {
        public override IEnumerable<UnityEvent> callbacks 
            => new[] { TriggerManager.Instance?.onUpdate };
    }
}

