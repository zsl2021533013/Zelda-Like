using System.Linq;
using Controller.Character.Enemy;
using Controller.Character.Player.Player;
using Controller.Combat;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Command
{
    public enum AttackType
    {
        Melee,
        Range
    }
    
    public class TryHurtPlayerCommand : AbstractCommand
    {
        public IParried attacker;
        public RaycastHit info;
        
        protected override void OnExecute()
        {
            var model = this.GetModel<IPlayerModel>();
            var status = model.components.Get<PlayerStatus>();

            if (status.isParrying)
            {
                attacker.Parried();
            }
            else
            {
                var hitParticle = Resources.Load<GameObject>("Art/Particle/Hit");
                hitParticle.Instantiate().Position(info.point);
            }
        }
    }
}