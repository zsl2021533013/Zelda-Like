using System;
using System.Linq;
using Controller.UI;
using QFramework;
using Tools.Dialogue_Graph.Node.Runtime;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using Tools.Dialogue_Graph.Runtime.Data;
using Tools.Dialogue_Graph.Utils;
using UnityEngine.Events;

namespace Tools.Dialogue_Graph.Runtime.Manager
{
    public class DialogueManger : Singleton<DialogueManger>
    {
        private DialogueGraph graph;
        public RootNode root;

        private DialoguePanel panel;

        #region Callback

        private UnityEvent onStart = new UnityEvent();
        private UnityEvent onComplete = new UnityEvent();

        #endregion
        
        private DialogueManger() {}

        public void StartDialogue(DialogueGraph graph, Action onStart = null, Action onComplete = null)
        {
            this.graph = graph;
            root = graph.nodes.FirstOrDefault(node => node is RootNode) as RootNode;
            
            this.onStart.RemoveAllListeners();
            this.onComplete.RemoveAllListeners();
            
            this.onStart.AddListener(() => onStart?.Invoke());
            this.onComplete.AddListener(() => onComplete?.Invoke());

            panel = UIKit.OpenPanel<DialoguePanel>();
            
            this.onStart?.Invoke();
            
            ProcessNode(root);
        }

        public void CompleteDialogue()
        {
            this.onComplete?.Invoke();
            UIKit.GetPanel<DialoguePanel>().TryClose();
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