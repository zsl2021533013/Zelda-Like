using System.Linq;
using Behaviour_Tree.Node.Editor;
using Behaviour_Tree.Node.Runtime.Root;
using GraphProcessor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Behaviour_Tree.Editor
{
    public class BehaviourTreeGraphView : BaseGraphView
    {
        public RootNode root { get; private set; }

        public BehaviourTreeGraphView(EditorWindow window) : base(window)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Behaviour Tree Graph View"));
            
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
    }
}