using System;
using System.Collections.Generic;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.Behaviour_Tree.Utils
{
    public static class BehaviourTreeNodeExtension
    {
        public static List<BehaviourTreeNode> GetChildren(this BehaviourTreeNode node)
        {
            var outputNodes = node.GetOutputNodes().Cast<BehaviourTreeNode>().ToList();
            
            // 编辑器内，x 越小，位置越靠左
            outputNodes.Sort((node1, node2) => node1.position.position.x < node2.position.position.y ? 1 : -1);

            return outputNodes;
        }
        
        public static void Traverse(this BehaviourTreeNode node, Action<BehaviourTreeNode> visitor) {
            if (node != null) 
            {
                visitor.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visitor));
            }
        }
    }
    
    public static class DictionaryExtension
    {
        public static Dictionary<Type, Object> Add<T>(this Dictionary<Type, Object> dict, Object component) where T : Object
        {
            dict[typeof(T)] = component;
            return dict;
        }
        
        public static T Get<T>(this Dictionary<Type, Object> dict) where T : Object
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
}