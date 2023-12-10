using System.Linq;
using Controller.Character.Enemy;
using Controller.Environment;
using Model.Interface;
using QFramework;

namespace Command
{
    public class TimeResetCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var enemyModel = this.GetModel<IEnemyModel>();
            enemyModel.enemyDict.Values
                .Select(component => component.Get<EnemyController>())
                .ForEach(controller =>
                {
                    var status = enemyModel.GetEnemyStatus(controller.transform);
                    status.isStopped.ResetWithoutEvent();
                    controller.TimeReset();
                });
  
            var fireballModel = this.GetModel<IFireballModel>();
            fireballModel.fireballDict.Values
                .Select(component => component.Get<FireballBase>())
                .ForEach(controller => controller.TimeReset());
        }
    }
}