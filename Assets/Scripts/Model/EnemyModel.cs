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
            
            InitEnemyStatus(transform, enemyStatus);
            
            enemyDict.TryAdd(transform, components);

            enemyDict[transform].Add(transform);
            enemyDict[transform].Add(enemyStatus);
            args.ForEach(arg => enemyDict[transform].Add(arg));
            
            Debug.Log($"{transform.name} Has Been Registered!");
        }

        private void InitEnemyStatus(Transform transform, EnemyStatus enemyStatus)
        {
            enemyStatus.isParried.Register(value =>
            {
                if (value)
                {
                    var questionMark = Resources.Load<GameObject>("Art/Particle/Stun");
                    questionMark
                        .Instantiate()
                        .Parent(transform)
                        .LocalPosition(new Vector3(0, enemyDict[transform].Get<CapsuleCollider>().height / 2, 0))
                        .Name("Stun");
                }
            });
            
            enemyStatus.state.Register(value =>
            {
                switch (value)
                {
                    case EnemyStatus.State.Alert:
                        var questionMark = Resources.Load<GameObject>("Art/Particle/QuestionMark");
                        questionMark
                            .Instantiate()
                            .Parent(transform)
                            .LocalPosition(new Vector3(0, enemyDict[transform].Get<CapsuleCollider>().height / 2, 0))
                            .Name("QuestionMark");
                        break;
                    case EnemyStatus.State.Combat:
                        var exclamationMark = Resources.Load<GameObject>("Art/Particle/ExclamationMark");
                        exclamationMark
                            .Instantiate()
                            .Parent(transform)
                            .LocalPosition(new Vector3(0, enemyDict[transform].Get<CapsuleCollider>().height / 2, 0))
                            .Name("ExclamationMark");;
                        break;
                }
            });
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