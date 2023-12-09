using System.Linq;
using QFramework;
using QFramework.Example;
using Tools.Behaviour_Tree.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Level_Editor.Runtime
{
    public class PlayerInteractCondition : ConditionBase
    {
        public Transform triggerArea;
        public Transform point;
        
        public override void OnEnable()
        {
            triggerArea.MonoInterface()
                .RegisterDrawGizmos(() =>
                {
                    Gizmos.matrix = Matrix4x4.TRS(triggerArea.position, triggerArea.rotation, triggerArea.localScale);
                    Gizmos.color = PlayerInArea() ? Color.green : Color.red;
                    Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                    Gizmos.matrix = Matrix4x4.identity;
                });

            TriggerManager.Instance.RegisterInteractableTrigger(controller,
                new TriggerManager.InteractableTriggerInfo()
                {
                    interactPoint = point,
                    condition = PlayerInArea,
                });
        }

        public override void OnDisable()
        {
            TriggerManager.Instance?.UnregisterInteractableTrigger(controller);
        }

        private bool PlayerInArea()
        {
            var colliders = Physics.OverlapBox(triggerArea.position, triggerArea.localScale / 2f, triggerArea.rotation);
            return colliders.FirstOrDefault(collider => collider.CompareTag("Player"));
        }

        public override bool Satisfied()
        {
            return Input.GetKeyDown(KeyCode.E) && PlayerInArea();
        }
    }
}