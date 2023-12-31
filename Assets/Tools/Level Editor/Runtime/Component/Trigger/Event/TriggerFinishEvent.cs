﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Level_Editor.Runtime.Event
{
    public class TriggerFinishEvent : EventBase
    {
        public List<TriggerController> controllers;

        public override IEnumerable<UnityEvent> callbacks
            => controllers.Select(controller => controller.onTriggerFinish);

        public override IEnumerable<Transform> connections
            => controllers.Select(controller => controller.transform);

    }
}