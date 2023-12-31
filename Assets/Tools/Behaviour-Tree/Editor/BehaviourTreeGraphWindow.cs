﻿using System.Linq;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Root;
using Tools.Behaviour_Tree.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace Tools.Behaviour_Tree.Editor
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
            titleContent = new GUIContent(graph.name);

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