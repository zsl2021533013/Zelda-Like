using System.Collections.Generic;
using System.Linq;
using Command;
using Data.Combat;
using Level_Editor.Runtime;
using Model.Interface;
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

            #region Enemy
            
            var enemy = colliders.FirstOrDefault(col => col.CompareTag("Enemy"));
            if (enemy)
            {
                this.SendCommand(new TryHurtEnemyCommand()
                {
                    enemy = enemy.transform,
                    attackerData = this.GetModel<IPlayerModel>().components.Get<CharacterCombatData>(),
                    /*attackPoint = targetInfo.point*/
                });
                
                return;
            }

            #endregion

            #region Trigger

            var trigger = colliders.FirstOrDefault(col => col.CompareTag("Trigger"));
            if (trigger)
            {
                var controllers = trigger.transform.GetComponents<TriggerController>();
                controllers.ForEach(controller => controller.tryTrigger?.Invoke());
                
                return;
            }

            #endregion
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