using System.Linq;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;

namespace Level_Editor.Runtime
{
    public class PlayerInAreaCondition : ConditionBase
    {
        public Transform transform;

        public override void OnEnable()
        {
            transform.MonoInterface()
                .RegisterDrawGizmos(() =>
                {
                    Gizmos.color = Satisfied() ? Color.green : Color.red;
                    Gizmos.DrawWireCube(transform.position, transform.localScale);
                });
        }

        public override bool Satisfied()
        {
            var colliders = Physics.OverlapBox(transform.position, transform.localScale);
            return colliders.FirstOrDefault(collider => collider.CompareTag("Player"));
        }
    }
}