using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Command
{
    public class StabEnemyCommand : AbstractCommand
    {
        public Transform enemy;
        
        protected override void OnExecute()
        {
            var enemyStatus = this.GetModel<IEnemyModel>().GetComponents(enemy).Get<EnemyStatus>();
            enemyStatus.isBackStabbed.Value = true;
        }
    }
}