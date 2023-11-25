using System;
using System.Collections.Generic;
using Data.Character.Enemy;
using QFramework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Model.Interface
{
    public class Components
    {
        public Dictionary<Type, Object> components { get; private set; } = new Dictionary<Type, Object>();
        
        public Components Add<T>(Object component) where T : Object
        {
            components[typeof(T)] = component;
            return this;
        }
        
        public Components Add(Object component)
        {
            var type = component.GetType();
            components[type] = component;
            return this;
        }
        
        public T Get<T>() where T : Object
        {
            if (components.TryGetValue(typeof(T), out var comp))
            {
                return (T)comp;
            }
            else
            {
                Debug.LogError($"Component of type {typeof(T)} not found in dictionary.");
                return null;
            }
        }
    }
    
    public interface IEnemyModel : IModel
    {
        public Dictionary<Transform, Components> enemyDict { get; }

        public void RegisterEnemy(Transform transform, params Object[] args);

        public void UnregisterEnemy(Transform transform);

        public Components GetComponents(Transform transform);
        
        public EnemyStatus GetEnemyStatus(Transform transform);
    }
}