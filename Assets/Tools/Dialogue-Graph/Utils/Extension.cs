using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using UnityEngine.InputSystem;

namespace Tools.Dialogue_Graph.Utils
{
    public static class DialogueGraphNodeExtension
    {
        public static List<DialogueGraphNode> GetChildren(this DialogueGraphNode node)
        {
            var outputNodes = node.GetOutputNodes()?.Cast<DialogueGraphNode>().ToList();

            // 编辑器内，x 越小，位置越靠左
            outputNodes.Sort((node1, node2) => node1.position.position.x > node2.position.position.x ? 1 : -1);

            return outputNodes;
        }
        
        
        public static List<DialogueGraphNode> GetChildren(this DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree)
        {
            return tree[node];
        }
        
        public static DialogueGraphNode GetParent(this DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree)
        {
            var pair = tree.ToList().FirstOrDefault(pair =>
            {
                var key = pair.Key;
                var value = pair.Value;

                return value.Contains(node);
            });
            
            return pair.Key;
        }
        
        public static List<DialogueGraphNode> GetBrother(this DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree)
        {
            var parent = node.GetParent(tree);
            
            return parent.GetChildren(tree).Where(child => child != node).ToList();
        }
        
        public static DialogueGraphNode GetLeftBrother(this DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree)
        {
            if (node.GetParent(tree) == null)
            {
                return null;
            }
            
            var parent = node.GetParent(tree);
            var children = parent.GetChildren(tree);

            DialogueGraphNode leftBro = null;
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
        
        public static DialogueGraphNode GetLeftMostNode(this DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree)
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
            }, tree);
            
            return leftMostNode;
        }
        
        public static DialogueGraphNode GetRightMostNode(this DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree)
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
            }, tree);
            
            return rightMostNode;
        }
        
        public static List<DialogueGraphNode> GetLeftBrothers(this DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree)
        {
            var leftBros = new List<DialogueGraphNode>();
            
            if (node.GetParent(tree) == null)
            {
                return leftBros;
            }
            
            var parent = node.GetParent(tree);
            var children = parent.GetChildren(tree);
            
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
        
        public static void Traverse(this DialogueGraphNode node, Action<DialogueGraphNode> visitor, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree) {
            if (node != null) 
            {
                visitor.Invoke(node);
                var children = node.GetChildren(tree);
                children.ForEach((n) => Traverse(n, visitor, tree));
            }
        }
    }
}