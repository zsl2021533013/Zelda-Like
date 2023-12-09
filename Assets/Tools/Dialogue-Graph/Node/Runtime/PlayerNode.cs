using System;
using GraphProcessor;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Dialogue_Graph.Node.Runtime
{
    [Serializable, NodeMenuItem("Dialogue/Player")]
    public class PlayerNode : DialogueGraphNode
    {
        [Input(allowMultiple: true), Vertical, HideInInspector] 
        public DialogueGraphLink input;
        
        [Output(allowMultiple: true), Vertical, HideInInspector]
        public DialogueGraphLink output;
        
        public override string name => "Player";

        public override string layoutStyle => "Dialogue Graph/PlayerNodeStyle";
    }
}