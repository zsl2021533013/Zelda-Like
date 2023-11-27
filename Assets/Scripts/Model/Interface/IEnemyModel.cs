using System;
using System.Collections.Generic;
using Data.Character.Enemy;
using QFramework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Model.Interface
{
    public interface IEnemyModel : IModel
    {
        public Dictionary<Transform, Components> enemyDict { get; }

        public void RegisterEnemy(Transform transform, params Object[] args);

        public void UnregisterEnemy(Transform transform);

        public Components GetComponents(Transform transform);
        
        public EnemyStatus GetEnemyStatus(Transform transform);
    }
}