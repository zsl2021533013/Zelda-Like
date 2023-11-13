using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class PlayAnimationAction : ActionBase
    {
        public Animator animator;
        
        public override void Perform(TriggerController controller)
        {
            animator.Play("Disappear");
        }
    }
}