using System;
using System.Text.RegularExpressions;
using GraphProcessor;
using QFramework;
using UnityEngine;

namespace Tools.Dialogue_Graph.Node.Runtime.Core
{
    [Serializable]
    public class DialogueGraphNode : BaseNode, IDialogueGraphNode, IController
    {
        [ShowInInspector] 
        public string text;
        
        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}