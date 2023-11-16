using UnityEngine;

namespace Tools.Behaviour_Tree.Utils
{
    public interface ITimer
    {
        float Elapsed {
            get;
        }

        void Reset();
    }

    public class AnimationTimer : ITimer
    {
        private float mStartTime;
        private float mAnimationTime;
        
        public float Elapsed => Time.time - mStartTime;
        public bool IsAnimatorFinish => Elapsed >= mAnimationTime;

        public AnimationTimer(float animationLength = 0f)
        {
            mStartTime = Time.time;
            mAnimationTime = animationLength;
        }

        public void Reset()
        {
            mStartTime = Time.time;
        }

        public static implicit operator float(AnimationTimer timer) => timer.Elapsed;
    }
}