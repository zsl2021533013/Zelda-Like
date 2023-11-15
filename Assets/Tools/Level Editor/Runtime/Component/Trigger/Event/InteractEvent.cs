using System.Collections.Generic;
using Controller.Character.Player.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Level_Editor.Runtime.Event
{
    public class InteractEvent : EventBase
    {
        public override IEnumerable<UnityEvent> callbacks 
            => new[] { Object.FindObjectOfType<PlayerController>()?.onUpdate };
    }
}

