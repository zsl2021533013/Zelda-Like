using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using Tools.Dialogue_Graph.Node.Runtime;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using Tools.Dialogue_Graph.Utils;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tools.Dialogue_Graph.Editor
{
    public class DialogueGraphView : BaseGraphView
    {
        private const float NodeDeltaX = 50f;
        private const float NodeDeltaY = 120f;
        
        public RootNode root { get; set; }
        
        public DialogueGraphView(EditorWindow window) : base(window)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Dialogue Graph View"));
            
            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }
        
        public RootNode CreateRootNode()
        {
            if (root != null)
            {
                return root;
            }
            
            root = BaseNode.CreateFromType<RootNode>(new Vector2(10, 200));

            AddNode(root);

            return root;
        }
        
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Align Tree", action => AlignTree());
            base.BuildContextualMenu(evt);
        }
        
        public void AlignTree()
        {
            var tree = BuildTree(root);
            
            // 有时节点的调整不会进入一种稳定的状态，但是概率较低，循环几次即可

            FirstAlignTraverse(root, tree);
            
            SecondAlignTraverse(root, tree);
            
            
            ReloadView();
        }

        private Dictionary<DialogueGraphNode, List<DialogueGraphNode>> BuildTree(DialogueGraphNode root)
        {
            var queue = new Queue<DialogueGraphNode>();
            var flag = new Dictionary<DialogueGraphNode, bool>();
            var ans = new Dictionary<DialogueGraphNode, List<DialogueGraphNode>>();
            
            queue.Enqueue(root);
            flag.Add(root, true);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                
                var children = node.GetChildren()
                    .Where(child =>
                    {
                        if (!flag.ContainsKey(child))
                        {
                            flag[child] = false;
                        }
                        
                        return !flag[child];
                    })
                    .ToList();
                ans[node] = children;
                
                children.ForEach(child =>
                {
                    flag[child] = true;
                    queue.Enqueue(child);
                });
            }

            return ans;
        }

        // 第一次遍历，奠定基础框架
        private void FirstAlignTraverse(DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree, int depth = 0)
        {
            node.position.position = new Vector2(node.position.x, NodeDeltaY * depth);
            
            var children = node.GetChildren(tree);
            var leftBro = node.GetLeftBrother(tree);
            
            if (children.Count <= 0)
            {
                if (leftBro != null)
                {
                    var leftBroRect = leftBro.position;
                    
                    var rect = node.position;
                    rect.position = leftBroRect.position + new Vector2(leftBroRect.size.x + NodeDeltaX, 0f);
                    
                    node.position = rect;
                }
                else
                {
                    var rect = node.position;
                    rect.center = new Vector2(0f, rect.center.y);
                    
                    node.position = rect;
                }
            }
            else
            {
                foreach (var child in children)
                {
                    FirstAlignTraverse(child, tree, depth + 1);
                }
                
                if (leftBro != null)
                {
                    var midpoint = 
                        (children[0].position.center.x + children[^1].position.center.x) / 2;
                
                    var leftBroRect = leftBro.position;
                    
                    var rect = node.position;
                    
                    rect.center = new Vector2(midpoint, leftBroRect.center.y);
                    
                    node.position = rect;
                    
                    var targetPos = leftBroRect.position.x + leftBroRect.size.x + NodeDeltaX;
                    
                    var delta = targetPos + rect.size.x / 2 - midpoint;
                    
                    node.Traverse(node =>
                    {
                        var rect = node.position;
                        rect.position += new Vector2(delta, 0);
                        node.position = rect;
                    }, tree);
                }
                else
                {
                    var midpoint = 
                        (children[0].position.center.x + children[^1].position.center.x) / 2;
                
                    var rect = node.position;
                    rect.center = new Vector2(midpoint, rect.center.y);
                    
                    node.position = rect;
                }
            }
        }
        
        // 第二次遍历，微调结构，避免线的交叉
        private void SecondAlignTraverse(DialogueGraphNode node, Dictionary<DialogueGraphNode, List<DialogueGraphNode>> tree)
        {
            var children = node.GetChildren(tree);
            var leftBro = node.GetLeftBrother(tree);

            foreach (var child in children)
            {
                SecondAlignTraverse(child, tree);
            }
            
            for (var i = 0; i < children.Count; i++)
            {
                var rightMostNode = children[i].GetRightMostNode(tree);
                for (var j = i + 1; j < children.Count; j++)
                {
                    var leftMostNode = children[j].GetLeftMostNode(tree);
                    
                    if (rightMostNode.position.x > leftMostNode.position.x)
                    {
                        var delta = rightMostNode.position.x + NodeDeltaX + rightMostNode.position.size.x 
                                    - leftMostNode.position.x;
                        for (var k = j; k < children.Count; k++)
                        {
                            var child = children[k];
                            child.Traverse(node =>
                            {
                                var rect = node.position;
                                rect.position += new Vector2(delta, 0);
                                node.position = rect;
                            }, tree);
                        }
                    }
                }

                #region Adjust Self

                node.position.center = 
                    new Vector2((children[0].position.center.x + children[^1].position.center.x) / 2, node.position.center.y);
                
                #endregion
            }
        }
    }
}