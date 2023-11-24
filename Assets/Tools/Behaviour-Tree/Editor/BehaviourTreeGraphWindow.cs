using System;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Root;
using Behaviour_Tree.Runtime;
using GraphProcessor;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Behaviour_Tree.Editor
{
    public class BehaviourTreeGraphWindow : BaseGraphWindow
    {
        private BehaviourTreeGraph _graph;

        [MenuItem("Window/Behaviour Tree")]
        public static BaseGraphWindow OpenBehaviourTreeGraphWindow()
        {
            var graphWindow = CreateWindow<BehaviourTreeGraphWindow>();

            graphWindow._graph = CreateInstance<BehaviourTreeGraph>();
            graphWindow._graph.hideFlags = HideFlags.HideAndDontSave;
            graphWindow.InitializeGraph(graphWindow._graph);

            graphWindow.Show();

            return graphWindow;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            graphView?.Dispose();
            
            DestroyImmediate(_graph);
        }

        protected override void InitializeWindow(BaseGraph graph)
        {
            titleContent = new GUIContent("Behaviour Tree");

            if (graphView == null)
            {
                graphView = new BehaviourTreeGraphView(this);
                graphView.Add(new BehaviourTreeToolBarView(graphView));
            }
            
            rootView.Add(graphView);
        }

        protected override void InitializeGraphView(BaseGraphView view)
        {
            var behaviourTreeView = (BehaviourTreeGraphView)view;
            
            if (behaviourTreeView == null)
            {
                return;
            }

            if (graph.nodes.FirstOrDefault(node => node is RootNode) is not RootNode root)
            {
                root = behaviourTreeView.CreateRootNode();
            }

            behaviourTreeView.root = root;
        }

        private void OnInspectorUpdate()
        {
            (graphView as BehaviourTreeGraphView)?.UpdateNodeColor();
        }
    }
}