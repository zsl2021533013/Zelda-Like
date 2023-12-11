using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class WaitForSecondsAction : ActionBase
    {
        public float duration;
        
        private float startTime;

        public override void OnEnter()
        {
            startTime = Time.time;
        }

        public override bool CanExit()
        {
            return startTime + duration < Time.time;
        }
    }
}