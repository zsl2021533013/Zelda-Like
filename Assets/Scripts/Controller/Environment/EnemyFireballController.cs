using System.Collections.Generic;
using System.Linq;
using Command;
using Controller.Character.Player.Player;
using Controller.Combat;
using Model.Interface;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controller.Environment
{
    public class EnemyFireballController : FireballBase, IParried
    {
        private Transform attacker;

        public override void Init(Vector3 target, params Object[] args)
        {
            base.Init(target, args);
            attacker = (Transform)args[0];
        }

        public override void OnCollision(List<Collider> colliders)
        {
            Destroy(gameObject);

            if (colliders.FirstOrDefault(col => col.CompareTag("Player")))
            {
                var model = this.GetModel<IPlayerModel>();
                var status = model.components.Get<PlayerStatus>();

                if (status.isParrying)
                {
                    Parried();
                    return;
                }

                Debug.Log("Detect Player");
            }
            
            var explosionParticle = Resources.Load<GameObject>("Art/Particle/Explosion");
            explosionParticle.Instantiate().Position(transform.position);
        }

        public void Parried()
        {
            this.SendCommand(new SpawnPlayerFireballCommand()
            {
                position = transform.position,
                rotation = transform.rotation,
                target = attacker.position
            });
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