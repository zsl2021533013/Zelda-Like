using System;
using System.Collections.Generic;
using Controller.Character.Enemy;
using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Model
{
    public class EnemyModel : AbstractModel, IEnemyModel
    {
        public Dictionary<Transform, Components> enemyDict { get; private set; } 
            = new Dictionary<Transform, Components>();
        
        protected override void OnInit()
        {
            
        }
        
        public void RegisterEnemy(Transform transform, params Object[] args)
        {
            var enemyStatus = ScriptableObject.CreateInstance<EnemyStatus>();
            var components = new Components();
            
            enemyDict.TryAdd(transform, components);

            enemyDict[transform].Add(transform);
            enemyDict[transform].Add(enemyStatus);
            args.ForEach(arg => enemyDict[transform].Add(arg));
            
            Debug.Log($"{transform.name} Has Been Registered!");
        }
        
        public void UnregisterEnemy(Transform transform)
        {
            if (enemyDict.ContainsKey(transform))
            {
                enemyDict.Remove(transform);
            }
            
            Debug.Log($"{transform.name} Has Been Unregistered!");
        }

        public Components GetComponents(Transform transform)
        {
            enemyDict.TryGetValue(transform, out var components);
            return components;
        }

        public EnemyStatus GetEnemyStatus(Transform transform)
        {
            enemyDict.TryGetValue(transform, out var components);
            
            return components?.Get<EnemyStatus>();
        }
    }
}