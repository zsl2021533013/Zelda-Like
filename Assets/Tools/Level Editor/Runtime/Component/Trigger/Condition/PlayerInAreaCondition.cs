using System.Linq;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level_Editor.Runtime
{
    public class PlayerInAreaCondition : ConditionBase
    {
        public Transform triggerArea;

        public override void OnEnable()
        {
            triggerArea.MonoInterface()
                .RegisterDrawGizmos(() =>
                {
                    Gizmos.matrix = Matrix4x4.TRS(triggerArea.position, triggerArea.rotation, triggerArea.localScale);
                    Gizmos.color = Satisfied() ? Color.green : Color.red;
                    Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                    Gizmos.matrix = Matrix4x4.identity;
                });
        }

        public override bool Satisfied()
        {
            var colliders = Physics.OverlapBox(triggerArea.position, triggerArea.localScale / 2f, triggerArea.rotation);
            return colliders.FirstOrDefault(collider => collider.CompareTag("Player"));
        }
    }
}