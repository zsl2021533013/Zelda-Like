﻿using System;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;
using Tools.Behaviour_Tree.Node.Runtime.Action.Base;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Lose Player")]
    public class LosePlayerNode : ActionNode
    {
        public override void OnStart()
        {
            this.GetModel<IEnemyModel>().GetEnemyStatus(transform).state.Value = EnemyStatus.State.Safe;
        }

        public override Status OnUpdate()
        {
            return Status.Success;
        }
    }
}