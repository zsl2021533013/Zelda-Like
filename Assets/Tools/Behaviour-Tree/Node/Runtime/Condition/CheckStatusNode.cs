using System;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using Sirenix.OdinInspector;
using Tools.Behaviour_Tree.Node.Runtime.Condition.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    [Serializable, NodeMenuItem("Behaviour/Condition/Check Status")]
    public class CheckStatusNode : ConditionNode
    {
        public enum Mode
        {
            State,
            StatusProperty
        }

        [GraphProcessor.ShowInInspector]
        public Mode mode = Mode.State;

        [GraphProcessor.ShowInInspector] 
        [ShowIf("mode", Mode.State)]
        public EnemyStatus.State stateType = EnemyStatus.State.Safe;
        
        [GraphProcessor.ShowInInspector] 
        [ShowIf("mode", Mode.StatusProperty)]
        public EnemyStatus.StatusProperty propertyType = EnemyStatus.StatusProperty.None;

        public override Status OnUpdate()
        {
            if (mode == Mode.State)
            {
                switch (stateType)
                {
                    case EnemyStatus.State.Safe:
                        if (enemyStatus.state == EnemyStatus.State.Safe)
                        {
                            return Status.Success;
                        }
                        return Status.Failure;
                    case EnemyStatus.State.Alert:
                        if (enemyStatus.state == EnemyStatus.State.Alert)
                        {
                            return Status.Success;
                        }
                        return Status.Failure;
                    case EnemyStatus.State.Combat:
                        if (enemyStatus.state == EnemyStatus.State.Combat) {
                            return Status.Success;
                        }
                        return Status.Failure;
                }
            }
            else
            {
                switch (propertyType)
                {
                    case EnemyStatus.StatusProperty.Stabbed:
                        if (enemyStatus.isStabbed)
                        {
                            enemyStatus.isStabbed.Reset();
                            return Status.Success;
                        }
                        return Status.Failure;
                    case EnemyStatus.StatusProperty.Parried:
                        if (enemyStatus.isParried)
                        {
                            return Status.Success;
                        }
                        return Status.Failure;
                    case EnemyStatus.StatusProperty.BackStabbed:
                        if (enemyStatus.isBackStabbed)
                        {
                            enemyStatus.isBackStabbed.Reset();
                            return Status.Success;
                        }
                        return Status.Failure;
                    case EnemyStatus.StatusProperty.Dead:
                        if (enemyStatus.isDead)
                        {
                            enemyStatus.isDead.Reset();
                            return Status.Success;
                        }
                        return Status.Failure;
                }
            }

            return Status.Failure;
        }
    }
}