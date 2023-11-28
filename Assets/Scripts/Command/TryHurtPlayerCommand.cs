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
        // public AttackType type;
        
        protected override void OnExecute()
        {
            var model = this.GetModel<IPlayerModel>();
            var status = model.components.Get<PlayerStatus>();
            
            // switch (type)
            // {
            //     case AttackType.Melee:
            //         if (status.isParrying)
            //         {
            //             Debug.Log("Parried");
            //             var enemyModel = this.GetModel<IEnemyModel>();
            //             enemyModel.enemyDict.Values
            //                 .Select(components => components.Get<EnemyController>())
            //                 .ForEach(controller => controller.Parried());
            //         }
            //         break;
            //     case AttackType.Range:
            //         attacker.Parried();
            //         break;
            // }

            if (status.isParrying)
            {
                attacker.Parried();
            }
        }
    }
}