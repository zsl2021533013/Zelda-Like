using Controller.Character.Enemy;
using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Command
{
    public class BackStabEnemyCommand : AbstractCommand
    {
        public Transform enemy;
        
        protected override void OnExecute()
        {
            var enemyStatus = this.GetModel<IEnemyModel>().GetComponents(enemy).Get<EnemyStatus>();
            enemyStatus.isBackStabbed.Set(true);
            enemyStatus.isParried.Reset();
            
            var enemyController = this.GetModel<IEnemyModel>().GetComponents(enemy).Get<EnemyController>();
            enemyController.TimeReset();
        }
    }
}