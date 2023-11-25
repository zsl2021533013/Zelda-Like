using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using Behaviour_Tree.Node.Runtime.Root;
using GraphProcessor;

namespace Behaviour_Tree.Runtime.Processor
{
    public class BehaviourTreeProcess : BaseGraphProcessor
    {
        public RootNode root { get; private set; }
        
        public BehaviourTreeProcess(BaseGraph graph) : base(graph)
        {
            root = graph.nodes.FirstOrDefault(node => node is RootNode) as RootNode;
        }

        public override void UpdateComputeOrder()
        {
        }

        // 舍弃不用
        public override void Run() { }
        
        public void Update()
        {
            root.Update();
        }
    }
}