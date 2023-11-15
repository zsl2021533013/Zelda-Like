using UnityEngine;

namespace Level_Editor.Runtime
{
    public class PlayerInteractCondition : ConditionBase
    {
        public override bool Satisfied()
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
}