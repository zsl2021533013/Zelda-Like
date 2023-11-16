using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.Behaviour_Tree.Utils
{
    public static class DictionaryExtension
    {
        public static Dictionary<Type, Component> Add<T>(this Dictionary<Type, Component> dict, Component component) 
            where T : Component
        {
            dict[typeof(T)] = component;
            return dict;
        }
        
        public static T Get<T>(this Dictionary<Type, Component> dict) where T : Component
        {
            if (dict.TryGetValue(typeof(T), out var comp))
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
    
    public static class TransformExtension
    {
        public static BehaviourTreeGizmosComp GizmosComp(this Transform transform)
        {
            var comp = transform.GetComponent<BehaviourTreeGizmosComp>() ?? 
                       transform.AddComponent<BehaviourTreeGizmosComp>();
            return comp;
        }
    }
}