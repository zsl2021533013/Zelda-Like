using System;
using System.Collections.Generic;
using System.Linq;
using Command;
using Controller.Combat;
using Level_Editor.Runtime;
using Model.Interface;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using Timer = Script.View_Controller.Character_System.HFSM.Util.Timer;

namespace Controller.Environment
{
    public class PlayerFireballController : FireballBase
    {
        public override void OnCollision(List<Collider> colliders)
        {
            enable = false;
            Destroy(gameObject);

            var trigger = colliders.FirstOrDefault(col => col.CompareTag("Trigger"));
            if (trigger)
            {
                var controller = trigger.transform.GetComponent<TriggerController>();
                controller.tryTrigger?.Invoke();
            }
        }

        public override void Stopped()
        {
            enable = false;
        }
    }
}