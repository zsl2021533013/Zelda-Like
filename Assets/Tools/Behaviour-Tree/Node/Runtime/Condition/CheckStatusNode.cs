﻿using System;
using Behaviour_Tree.Node.Runtime.Core;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    [Serializable, NodeMenuItem("Behaviour/Condition/Check Status")]
    public class CheckStatusNode : ConditionNode
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
                case EnemyStatusProperty.Stabbed:
                    if (enemyStatus.isBackStabbed)
                    {
                        enemyStatus.isBackStabbed.Reset();
                        return Status.Success;
                    }
                    return Status.Failure;
                case EnemyStatusProperty.Dead:
                    if (enemyStatus.isDead)
                    {
                        enemyStatus.isDead.Reset();
                        return Status.Success;
                    }
                    return Status.Failure;
                case EnemyStatusProperty.State_Safe:
                    if (enemyStatus.state == EnemyState.Safe)
                    {
                        return Status.Success;
                    }
                    return Status.Failure;
                case EnemyStatusProperty.State_Alert:
                    if (enemyStatus.state == EnemyState.Alert)
                    {
                        return Status.Success;
                    }
                    return Status.Failure;
                case EnemyStatusProperty.State_Combat:
                    if (enemyStatus.state == EnemyState.Combat)
                    {
                        return Status.Success;
                    }
                    return Status.Failure;
            }

            return Status.Failure;
        }
    }
}