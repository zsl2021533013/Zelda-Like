using Data.Combat;
using QFramework;
using UnityEngine;

namespace Command
{
    public class TryHurtEnemyCommand : AbstractCommand
    {
        public Transform enemy;
        public AttackerData data;
        
        protected override void OnExecute()
        {
            
        }
    }
}