using System.Linq;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Editor;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using Tools.Behaviour_Tree.Node.Runtime.Root;
using Tools.Behaviour_Tree.Utils;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tools.Behaviour_Tree.Editor
{
    public class BehaviourTreeGraphView : BaseGraphView
    {
        private const float NodeDeltaX = 50f;
        private const float NodeDeltaY = 120f;
        
        public RootNode root { get; set; }

        public BehaviourTreeGraphView(EditorWindow window) : base(window)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Behaviour Tree/BehaviourTreeGraphView"));
            
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

        public void UpdateNodeColor()
        {
            foreach (var nodeView in nodeViews.OfType<BehaviourTreeNodeView>())
            {
                nodeView.UpdateColor();
            }
        }
        
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Align Tree", action => AlignTree());
            base.BuildContextualMenu(evt);
        }
        
        public void AlignTree()
        {
            // 有时节点的调整不会进入一种稳定的状态，但是概率较低，循环几次即可
            for (var i = 0; i < 5; i++)
            {
                FirstAlignTraverse(root);
            
                SecondAlignTraverse(root);
            }
            
            ReloadView();
        }

        // 第一次遍历，奠定基础框架
        private void FirstAlignTraverse(BehaviourTreeNode node, int depth = 0)
        {
            node.position.position = new Vector2(node.position.x, NodeDeltaY * depth);
            
            var children = node.GetChildren();
            var leftBro = node.GetLeftBrother();
            
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
                    FirstAlignTraverse(child, depth + 1);
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
                    });
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
        private void SecondAlignTraverse(BehaviourTreeNode node)
        {
            var children = node.GetChildren();
            var leftBro = node.GetLeftBrother();

            foreach (var child in children)
            {
                SecondAlignTraverse(child);
            }
            
            for (var i = 0; i < children.Count; i++)
            {
                var rightMostNode = children[i].GetRightMostNode();
                for (var j = i + 1; j < children.Count; j++)
                {
                    var leftMostNode = children[j].GetLeftMostNode();
                    
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
                            });
                        }
                    }
                }

                #region Adjust Self

                node.position.center = 
                    new Vector2((children[0].position.center.x + children[^1].position.center.x) / 2, node.position.center.y);
                
                #endregion

                // #region Adjust Brother
                //
                // var leftBros = node.GetLeftBrothers();
                //
                // if (leftBros.Count <= 1)
                // {
                //     return;
                // }
                //
                // var newDeltaX = (node.position.x - leftBros[0].position.x);
                // leftBros.ForEach(bro => newDeltaX -= bro.position.size.x);
                // newDeltaX /= leftBros.Cou    nt;
                //
                // for (var j = 1; j < leftBros.Count; j++)
                // {
                //     var delta = (leftBros[j - 1].position.position +
                //                 new Vector2(leftBros[j - 1].position.size.x, 0) +
                //                 new Vector2(newDeltaX, 0)).x - 
                //                 leftBros[j].position.x;
                //     leftBros[j].Traverse(child =>
                //     {
                //         child.position.position += new Vector2(delta, 0);
                //     }); 
                // }
                //
                // #endregion
            }
        }
    }
}