using Controller.Environment;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Command
{
    public class SpawnEnemyFireballCommand : AbstractCommand
    {
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.identity;
        public Transform attacker = null;
        public Vector3 target = Vector3.zero;
        
        protected override void OnExecute()
        {
            var fireball = Resources.Load<GameObject>("Prefab/Enemy Fireball")
                .Instantiate()
                .Position(position)
                .Rotation(rotation);

            var controller = this.GetModel<IFireballModel>()
                .GetComponents(fireball.transform)
                .Get<FireballBase>();
            controller.Init(target, attacker);
        }
    }
}