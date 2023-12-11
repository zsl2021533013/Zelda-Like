using Command;
using QFramework;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class ShootFireballAction : ActionBase
    {
        public Transform spawnPoint;
        
        public override void OnEnter()
        {
            this.SendCommand(new SpawnEnemyFireballCommand()
            {
                position = spawnPoint.position,
                attacker = controller.transform,
                target = spawnPoint.position + spawnPoint.forward
            });
        }
    }
}