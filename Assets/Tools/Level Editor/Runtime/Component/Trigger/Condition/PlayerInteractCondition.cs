using QFramework;
using QFramework.Example;
using Unity.VisualScripting;
using UnityEngine;

namespace Level_Editor.Runtime
{
    public class PlayerInteractCondition : ConditionBase
    {
        public Transform trigger;
        
        public override void OnEnable()
        {
            var panel = UIKit.GetPanel<TriggerPanel>();
            if (panel == null)
            {
                panel = UIKit.OpenPanel<TriggerPanel>();
            }
            
            panel.AddTrigger(trigger);
        }

        public override bool Satisfied()
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
}