﻿using System;
using System.Linq;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Alert")]
    public class AlertNode : EnemyActionNode
    {
        public override Status OnUpdate()
        {
            var enemys = this.GetModel<IEnemyModel>().enemyDict.Keys
                .Where(enemy => Vector3.Distance(transform.position, enemy.position) < config.alertDist);

            enemys.ForEach(enemy =>
                {
                    var property = this.GetModel<IEnemyModel>().GetEnemyStatus(enemy).state;
                    if (property == EnemyState.Safe)
                    {
                        property.Set(EnemyState.Alert);
                    }
                }
            );
            
            return Status.Success;
        }
    }
}