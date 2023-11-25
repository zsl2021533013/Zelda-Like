using System;
using Behaviour_Tree.Node.Runtime.Core;
using Data.Character.Enemy;
using GraphProcessor;
using Model.Interface;
using QFramework;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Lose Player")]
    public class LosePlayerNode : EnemyActionNode
    {
        public override void OnStart()
        {
            this.GetModel<IEnemyModel>().GetEnemyStatus(transform).state.Value = EnemyState.Safe;
        }

        public override Status OnUpdate()
        {
            return Status.Success;
        }
    }
}