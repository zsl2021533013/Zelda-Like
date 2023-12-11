﻿using Controller.Character.Enemy;
using Data.Character.Enemy;
using Data.Combat;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Command
{
    public class TryHurtEnemyCommand : AbstractCommand
    {
        public Transform enemy;
        public AttackerData attackerData;
        
        protected override void OnExecute()
        {
            var components = this.GetModel<IEnemyModel>().GetComponents(enemy);
            
            var characterCombatData = components.Get<CharacterCombatData>();
            characterCombatData.health -= attackerData.attack;

            if (characterCombatData.health <= 0)
            {
                var status = components.Get<EnemyStatus>();
                status.isDead.Value = true;
            }
        }
    }
}