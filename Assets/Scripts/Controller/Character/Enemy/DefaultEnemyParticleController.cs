using Controller.Character.Enemy.Core;
using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Controller.Character.Enemy
{
    public class DefaultEnemyParticleController : EnemyParticleControllerBase
    {
        private GameObject particle;
        
        public override void Init(Transform enemy)
        {
            var enemyStatus = this.GetModel<IEnemyModel>().GetEnemyStatus(enemy);

            enemyStatus.isParried.Register(value =>
            {
                if (value)
                {
                    particle.DestroySelf();
                    
                    var questionMark = Resources.Load<GameObject>("Art/Particle/Stun");
                    particle = questionMark
                        .Instantiate()
                        .Parent(transform)
                        .LocalPosition(Vector3.zero)
                        .Name("Stun");
                }
            });

            enemyStatus.state.Register(value =>
            {
                particle.DestroySelf();
                
                switch (value)
                {
                    case EnemyStatus.State.Alert:
                        var questionMark = Resources.Load<GameObject>("Art/Particle/Question Mark");
                        particle = questionMark
                            .Instantiate()
                            .Parent(transform)
                            .LocalPosition(Vector3.zero)
                            .Name("Question Mark");
                        break;
                    case EnemyStatus.State.Combat:
                        var exclamationMark = Resources.Load<GameObject>("Art/Particle/Exclamation Mark");
                        particle = exclamationMark
                            .Instantiate()
                            .Parent(transform)
                            .LocalPosition(Vector3.zero)
                            .Name("Exclamation Mark");
                        break;
                }
            });
        }
    }
}