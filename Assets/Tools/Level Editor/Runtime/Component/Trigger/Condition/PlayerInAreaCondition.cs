using System.Linq;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;

namespace Level_Editor.Runtime
{
    public class PlayerInAreaCondition : ConditionBase
    {
        public Transform transform;
        public float radius;

        public override void OnEnable()
        {
            transform.GizmosComp()
                .RegisterDrawGizmos(() =>
                {
                    Gizmos.color = Satisfied() ? Color.green : Color.red;
                    Gizmos.DrawWireSphere(transform.position, radius);
                });
        }

        public override bool Satisfied()
        {
            var colliders = Physics.OverlapSphere(transform.position, radius);
            return colliders.FirstOrDefault(collider => collider.CompareTag("Player"));
        }
    }
}