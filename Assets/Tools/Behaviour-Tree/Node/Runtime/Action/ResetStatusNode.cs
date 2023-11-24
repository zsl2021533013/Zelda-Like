using System;
using Behaviour_Tree.Node.Runtime.Core;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;

namespace Behaviour_Tree.Node.Runtime.Action
{
    public class ResetStatusNode : EnemyActionNode
    {
        [ShowInInspector] 
        public EnemyStatusProperty property = EnemyStatusProperty.None;
        
        private EnemyStatus enemyStatus;

        public override void OnStart()
        {
            enemyStatus = this.GetModel<IEnemyModel>().GetComponents(transform).Get<EnemyStatus>();
        }
        
        public override Status OnUpdate()
        {
            switch (property)
            {
                case EnemyStatusProperty.None:
                    break;
                case EnemyStatusProperty.Stabbed:
                    enemyStatus.isStabbed.Reset();
                    break;
                case EnemyStatusProperty.Dead:
                    enemyStatus.isDead.Reset();
                    break;
                case EnemyStatusProperty.Alert:
                    enemyStatus.isAlert.Reset();
                    break;
                default:
                    break;
            }

            return Status.Success;
        }
    }
}