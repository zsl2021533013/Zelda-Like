using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class DebugAction : ActionBase
    {
        public override void OnEnter()
        {
            Debug.Log("Trigger Enter");
        }
    }
}