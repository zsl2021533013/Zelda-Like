using System.Collections.Generic;
using System.Linq;
using Level_Editor.Runtime;
using UnityEngine;

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

        public override void TimeStop()
        {
            enable = false;
        }

        public override void TimeReset()
        {
            enable = true;
        }
    }
}