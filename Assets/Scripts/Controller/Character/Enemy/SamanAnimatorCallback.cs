using Command;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Controller.Character.Enemy
{
    public class SamanAnimatorCallback : EnemyAnimatorCallback
    {
        public Transform fireballPoint;
        
        public void ShootFireball()
        {
            this.SendCommand(new SpawnEnemyFireballCommand()
            {
                position = fireballPoint.position,
                attacker = transform,
                target = this.GetModel<IPlayerModel>().components.Get<Transform>().position
            });
        }
    }
}