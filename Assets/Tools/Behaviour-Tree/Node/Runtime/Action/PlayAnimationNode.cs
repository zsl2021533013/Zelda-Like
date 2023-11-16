using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;

namespace Behaviour_Tree.Node.Runtime.Action
{
    [Serializable, NodeMenuItem("Behaviour/Action/Play Animation")]
    public class PlayAnimationNode : ActionNode
    {
        [Input("Animator", false)] public Animator animator;

        [ShowInInspector] public string animationName;

        private AnimationTimer _timer;

        public override void OnStart()
        {
            _timer = new AnimationTimer(animator.GetAnimationLength(animationName));
            
            animator.CrossFade(animationName, 0.1f);
        }

        public override Status OnUpdate()
        {
            if (_timer.IsAnimatorFinish)
            {
                return Status.Success;
            }

            return Status.Running;
        }
    }
}