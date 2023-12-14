using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class SetActiveAction : ActionBase
    {
        public bool isActive;
        public GameObject gameObject;
        
        public override void OnEnter()
        {
            gameObject.SetActive(isActive);
        }
    }
}