using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Alert")]
    public class AlertNode : EnemyActionNode
    {
        public override Status OnUpdate()
        {
            var enemys = this.GetModel<IEnemyModel>().enemyDict.Keys
                .Where(enemy => enemy != transform)
                .Where(enemy => Vector3.Distance(transform.position, enemy.position) < config.alertDist);

            enemys.ForEach(enemy =>
                    this.GetModel<IEnemyModel>().GetComponents(transform).Get<EnemyStatus>().isAlert.Value = true
            );
            
            return Status.Success;
        }
    }
}