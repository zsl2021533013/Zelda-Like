using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.Behaviour_Tree.Utils
{
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
                            
                            return clip.length;
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

    public static class DictionaryExtension
    {
        public static Dictionary<Type, Component> Add<T>(this Dictionary<Type, Component> dict, Component component) 
            where T : Component
        {
            dict[typeof(T)] = component;
            return dict;
        }
        
        public static T Get<T>(this Dictionary<Type, Component> dict) where T : Component
        {
            if (dict.TryGetValue(typeof(T), out var comp))
            {
                return (T)comp;
            }
            else
            {
                Debug.LogError($"Component of type {typeof(T)} not found in dictionary.");
                return null;
            }
        }
    }
}