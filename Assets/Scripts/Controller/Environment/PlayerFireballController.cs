using System.Collections.Generic;
using System.Linq;
using Level_Editor.Runtime;
using QFramework;
using UnityEngine;

namespace Controller.Environment
{
    public class PlayerFireballController : FireballBase
    {
        public override void OnCollision(List<Collider> colliders)
        {
            enable = false;
            Destroy(gameObject);
            
            var explosionParticle = Resources.Load<GameObject>("Art/Particle/Explosion");
            explosionParticle.Instantiate().Position(transform.position);

            var trigger = colliders.FirstOrDefault(col => col.CompareTag("Trigger"));
            if (trigger)
            {
                var controllers = trigger.transform.GetComponents<TriggerController>();
                controllers.ForEach(controller => controller.tryTrigger?.Invoke());
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