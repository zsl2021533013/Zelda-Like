using Controller.UI;
using QFramework;
using Script.View_Controller.Input_System;
using Tools.Dialogue_Graph.Runtime.Data;
using Tools.Dialogue_Graph.Runtime.Manager;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class StartDialogueAction : ActionBase
    {
        public DialogueGraph graph;

        private bool isComplete;

        public override void OnEnter()
        {
            base.OnEnter();
            
            isComplete = false;

            DialogueManger.Instance.StartDialogue(graph, onComplete: () => isComplete = true);

            Cursor.lockState = CursorLockMode.None;
            
            InputKit.Instance.DisablePlayerInput();
        }

        public override bool CanExit()
        {
            return isComplete;
        }

        public override void OnExit()
        {
            base.OnExit();
            
            Cursor.lockState = CursorLockMode.Locked;
            
            InputKit.Instance.EnablePlayerInput();
        }
    }
}