using System;
using GraphProcessor;
using Tools.Dialogue_Graph.Node.Runtime.Core;

namespace Tools.Dialogue_Graph.Node.Runtime
{
    [Serializable, NodeMenuItem("Dialogue/Aside")]
    public class AsideNode : DialogueGraphNode
    {
        [Input(allowMultiple: true), Vertical] 
        public DialogueGraphLink input;
        
        [Output(allowMultiple: true), Vertical]
        public DialogueGraphLink output;
        
        public override string name => "Aside";

        public override string layoutStyle => "Dialogue Graph/AsideNodeStyle";
    }
}