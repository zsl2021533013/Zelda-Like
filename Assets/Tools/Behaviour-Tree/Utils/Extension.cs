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
            outputNodes.Sort((node1, node2) => node1.position.position.x > node2.position.position.x ? 1 : -1);

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
}