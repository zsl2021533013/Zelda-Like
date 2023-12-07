using System.Linq;
using GraphProcessor;
using Tools.Dialogue_Graph.Node.Runtime;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using Tools.Dialogue_Graph.Runtime;
using Tools.Dialogue_Graph.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace Tools.Dialogue_Graph.Editor
{
    public class DialogueGraphWindow : BaseGraphWindow
    {
        private DialogueGraph _graph;

        /*[MenuItem("Window/Dialogue Graph")]
        public static BaseGraphWindow OpenDialoguGraphWindow()
        {
            var graphWindow = CreateWindow<DialogueGraphWindow>();

            graphWindow._graph = CreateInstance<DialogueGraph>();
            graphWindow._graph.hideFlags = HideFlags.HideAndDontSave;
            graphWindow.InitializeGraph(graphWindow._graph);

            graphWindow.Show();

            return graphWindow;
        }*/

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
                graphView = new DialogueGraphView(this);
                graphView.Add(new DialogueGraphToolBarView(graphView));
            }
            
            rootView.Add(graphView);
        }
        
        protected override void InitializeGraphView(BaseGraphView view)
        {
            var dialogueGraphView = (DialogueGraphView)view;
            
            if (dialogueGraphView == null)
            {
                return;
            }

            if (graph.nodes.FirstOrDefault(node => node is RootNode) is not RootNode root)
            {
                root = dialogueGraphView.CreateRootNode();
            }

            dialogueGraphView.root = root;
        }
    }
}