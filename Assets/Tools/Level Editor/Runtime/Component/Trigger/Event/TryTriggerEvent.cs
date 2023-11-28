using System.Collections.Generic;
using UnityEngine.Events;

namespace Level_Editor.Runtime.Event
{
    public class TryTriggerEvent : EventBase
    {
        public TriggerController controller;

        public override IEnumerable<UnityEvent> callbacks
            => new[] { controller.tryTrigger };

    }
}