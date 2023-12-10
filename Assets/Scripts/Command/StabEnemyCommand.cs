using Controller.Character.Enemy;
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
            enemyStatus.isStabbed.Value = true;
            enemyStatus.isParried.ResetWithoutEvent();
            enemyStatus.isStopped.ResetWithoutEvent();
            
            var enemyController = this.GetModel<IEnemyModel>().GetComponents(enemy).Get<EnemyController>();
            enemyController.TimeReset();
        }
    }
}