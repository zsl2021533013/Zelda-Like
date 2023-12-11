using QFramework;
using UnityEngine;

namespace Controller.Character.Enemy.Core
{
    public interface IEnemyParticleController
    {
        public void Init(Transform enemy);
    }
    
    public abstract class EnemyParticleControllerBase : MonoBehaviour, IEnemyParticleController, IController
    {
        public abstract void Init(Transform enemy);
        
        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}