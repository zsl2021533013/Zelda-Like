using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class DestroyAction : ActionBase
    {
        public GameObject gameObject;
        
        public override void OnEnter()
        {
            Object.Destroy(gameObject);
        }
    }
}