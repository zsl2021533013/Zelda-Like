using GraphProcessor;
using QFramework;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Dialogue_Graph.Node.Runtime
{
    public class RootNode : DialogueGraphNode
    {
        [Output("child", false), Vertical] 
        public DialogueGraphLink output;
        
        public override string name => "Root";

        public override string layoutStyle => "Dialogue Graph/RootNodeStyle";

        public override bool deletable => false;
    }
}