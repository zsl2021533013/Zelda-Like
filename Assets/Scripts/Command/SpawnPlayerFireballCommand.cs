using Controller.Environment;
using QFramework;
using UnityEngine;

namespace Command
{
    public class SpawnPlayerFireballCommand : AbstractCommand
    {
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.identity;
        public Vector3 target = Vector3.zero;
        
        protected override void OnExecute()
        {
            var fireball = Resources.Load<GameObject>("Prefab/Player Fireball")
                .Instantiate()
                .Position(position)
                .Rotation(rotation);
            fireball.GetComponent<PlayerFireballController>().InitFireball(target);
        }
    }
}