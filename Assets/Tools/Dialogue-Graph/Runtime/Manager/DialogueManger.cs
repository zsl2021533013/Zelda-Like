using System.Linq;
using Controller.UI;
using QFramework;
using Tools.Dialogue_Graph.Node.Runtime;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using Tools.Dialogue_Graph.Runtime.Data;
using Tools.Dialogue_Graph.Utils;

namespace Tools.Dialogue_Graph.Runtime.Manager
{
    public class DialogueManger : Singleton<DialogueManger>
    {
        private DialogueGraph graph;
        public RootNode root;

        private DialoguePanel panel;
        
        private DialogueManger() {}

        public void InitGraph(DialogueGraph graph)
        {
            this.graph = graph;
            root = graph.nodes.FirstOrDefault(node => node is RootNode) as RootNode;

            panel = UIKit.OpenPanel<DialoguePanel>();
            
            ProcessNode(root);
        }
        
        public void ProcessNode(DialogueGraphNode node)
        {
            if (node == null)
            {
                return;
            }
            
            var asideNodes = node.GetChildren().Where(node => node is AsideNode);
            var playerNodes = node.GetChildren().Where(node => node is PlayerNode);
            
            panel.InitDialoguePanel(asideNodes.FirstOrDefault(), playerNodes.ToList());
        }
    }
}