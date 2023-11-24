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
        
        public static BehaviourTreeNode GetParent(this BehaviourTreeNode node)
        {
            var inputNodes = node.GetInputNodes().Cast<BehaviourTreeNode>().ToList();
            
            return inputNodes.FirstOrDefault();
        }
        
        public static List<BehaviourTreeNode> GetBrother(this BehaviourTreeNode node)
        {
            var parent = node.GetParent();
            
            return parent.GetChildren().Where(child => child != node).ToList();
        }
        
        public static BehaviourTreeNode GetLeftBrother(this BehaviourTreeNode node)
        {
            if (node.GetParent() == null)
            {
                return null;
            }
            
            var parent = node.GetParent();
            var children = parent.GetChildren();

            BehaviourTreeNode leftBro = null;
            foreach (var child in children)
            {
                if (child == node)
                {
                    return leftBro;
                }
                
                leftBro = child;
            }
            
            return leftBro;
        }
        
        public static BehaviourTreeNode GetLeftMostNode(this BehaviourTreeNode node)
        {
            var leftMost = node.position.position.x;
            var leftMostNode = node;
            
            node.Traverse(mostNode =>
            {
                if (mostNode.position.position.x < leftMost)
                {
                    leftMost = mostNode.position.position.x;
                    leftMostNode = mostNode;
                }
            });
            
            return leftMostNode;
        }
        
        public static BehaviourTreeNode GetRightMostNode(this BehaviourTreeNode node)
        {
            var rightMost = node.position.position.x;
            var rightMostNode = node;
            
            node.Traverse(mostNode =>
            {
                if (mostNode.position.position.x > rightMost)
                {
                    rightMost = mostNode.position.position.x;
                    rightMostNode = mostNode;
                }
            });
            
            return rightMostNode;
        }
        
        public static List<BehaviourTreeNode> GetLeftBrothers(this BehaviourTreeNode node)
        {
            var leftBros = new List<BehaviourTreeNode>();
            
            if (node.GetParent() == null)
            {
                return leftBros;
            }
            
            var parent = node.GetParent();
            var children = parent.GetChildren();
            
            foreach (var child in children)
            {
                if (child == node)
                {
                    return leftBros;
                }
                
                leftBros.Add(child);
            }
            
            return leftBros;
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