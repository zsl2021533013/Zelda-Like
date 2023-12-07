using System.Linq;
using GraphProcessor;
using Tools.Dialogue_Graph.Node.Runtime;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using Tools.Dialogue_Graph.Utils;

namespace Tools.Dialogue_Graph.Runtime.Processor
{
    public class DialogueGraphProcess : BaseGraphProcessor
    {
        public RootNode root { get; private set; }
        private string currentGUID;
        
        public DialogueGraphProcess(BaseGraph graph) : base(graph)
        {
            root = graph.nodes.FirstOrDefault(node => node is RootNode) as RootNode;
         }

        public override void UpdateComputeOrder() { }
        
        public override void Run() { }
        
        public void ProcessGUID(string nodeGUID)
        {
            var node = graph.nodes.FirstOrDefault(node => node.GUID == nodeGUID) as DialogueGraphNode;
            
            if (node == null)
            {
                return;
            }
            
            var asideNodes = node.GetChildren().Where(node => node is AsideNode);
            var playerNodes = node.GetChildren().Where(node => node is PlayerNode);
        }
    }
}

