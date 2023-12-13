using Sirenix.OdinInspector;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class DestroyAction : ActionBase
    {
        public enum Type
        {
            Self,
            Other
        }

        public Type type;
        [ShowIf("type", Type.Other)] public GameObject gameObject;
        
        public override void OnEnter()
        {
            switch (type)
            {
                case Type.Self:
                    Object.Destroy(controller.gameObject);
                    break;
                case Type.Other:
                    Object.Destroy(gameObject);
                    break;
            }
        }
    }
}