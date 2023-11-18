using Unity.VisualScripting;
using UnityEngine;

namespace Tools.Behaviour_Tree.Utils
{
    public static class TransformExtension
    {
        public static UnityMonoInterface MonoInterface(this Transform transform)
        {
            var comp = transform.GetComponent<UnityMonoInterface>() ?? 
                       transform.AddComponent<UnityMonoInterface>();
            return comp;
        }
    }
}