using System;
using Script.View_Controller.Character_System.HFSM.Base;
using Script.View_Controller.Character_System.HFSM.States;
using Script.View_Controller.Character_System.HFSM.Transitions;
using UnityEditor.Animations;
using UnityEngine;

namespace Script.View_Controller.Character_System.HFSM.StateMachine
{
    public static class AnimationStateMachineShortcut
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fsm"></param>
        /// <param name="animator"></param>
        /// <param name="animationName"></param>
        /// <param name="onEnter"></param>
        /// <param name="onLogic"></param>
        /// <param name="onFixedLogic"></param>
        /// <param name="onExit"></param>
        /// <param name="canExit"></param>
        /// <param name="needsExitTime"></param>
        /// <param name="isGhostState"></param>
        /// <typeparam name="TState"></typeparam>
        public static void AddState<TState>(
            this StateMachine<Type, Type, Type> fsm,
            Animator animator = null,
            string animationName = null,
            Action<AnimationState<Type, Type>> onEnter = null,
            Action<AnimationState<Type, Type>> onLogic = null,
            Action<AnimationState<Type, Type>> onFixedLogic = null,
            Action<AnimationState<Type, Type>> onExit = null,
            Func<AnimationState<Type, Type>, bool> canExit = null,
            bool needsExitTime = false,
            bool isGhostState = false)
        {
            if (animator == null || animationName == null)
            {
                fsm.AddState(typeof(TState), new StateBase<Type>(needsExitTime));
                return;
            }
            
            fsm.AddState(typeof(TState), new AnimationState<Type, Type>
            (animator, animationName, animator.GetAnimationLength(animationName),
                onEnter, onLogic, onFixedLogic, onExit, canExit, needsExitTime, isGhostState));
        }

        /// <summary>
        /// Add transition from TState1 to TState2
        /// </summary>
        /// <param name="fsm">Your FSM</param>
        /// <param name="condition">Transition will happen when condition return true</param>
        /// <param name="onTransition">A function that will call when the transition happens</param>
        /// <param name="forceInstantly">If true, it will translate immediately</param>
        /// <typeparam name="TState1">The "from" state</typeparam>
        /// <typeparam name="TState2">The "to" state</typeparam>
        public static void AddTransition<TState1, TState2>(
            this StateMachine<Type, Type, Type> fsm,
            Func<Transition<Type>, bool> condition = null,
            Action<Transition<Type>> onTransition = null,
            bool forceInstantly = false)
        {
            fsm.AddTransition(new Transition<Type>(typeof(TState1), typeof(TState2), condition, onTransition, forceInstantly));
        }
    }
    
    public static class AnimatorExtension
    {
        /// <summary>
        /// Get target animation length
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="animationName"></param>
        /// <returns></returns>
        public static float GetAnimationLength(this Animator animator, string animationStateName)
        {
            var controller = animator.runtimeAnimatorController as AnimatorController;

            if (controller != null)
            {
                foreach (var layer in controller.layers)
                {
                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.name == animationStateName)
                        {
                            var clip = state.state.motion as AnimationClip;
                            
                            if (!clip)
                            {
                                return 0;
                            }
                            
                            return clip.length / state.state.speed;
                        }
                    }
                }
            }

            return 0;
            // var clips = animator.runtimeAnimatorController.animationClips;
            // return clips.Where(clip => clip.name.Equals(animationName))
            //     .Select(clip => clip.length)
            //     .FirstOrDefault();
        }
        
        /// <summary>
        /// Get the first animation length
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="animationName"></param>
        /// <returns></returns>
        public static float GetAnimationLength(this Animator animator)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            return clips[0].length;
        }
    }
}