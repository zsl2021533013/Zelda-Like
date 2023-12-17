using System.Collections.Generic;
using Tools.Level_Editor.Runtime.Component.Trigger;
using UnityEngine.Events;

namespace Level_Editor.Runtime.Event
{
    public class TriggerStayEvent : EventBase
    {
        public TriggerInterface trigger;
        
        public override IEnumerable<UnityEvent> callbacks
        {
            get
            {
                return new[] { trigger.onTriggerStay };
            }
        }
    }
}