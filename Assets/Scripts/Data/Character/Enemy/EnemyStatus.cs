using Data.Character.Base;
using QFramework;
using UnityEngine;

namespace Data.Character.Enemy
{
    public class EnemyStatus : ScriptableObject
    {
        public enum StatusProperty
        {
            None,
            Parried,
            Stabbed,
            Stopped,
            BackStabbed,
            Dead
        }

        public enum State
        {
            Safe,
            Alert,
            Combat
        }
        
        public BindableProperty<bool> isParried = new BindableProperty<bool>();
        public BindableProperty<bool> isStabbed = new BindableProperty<bool>();
        public BindableProperty<bool> isStopped = new BindableProperty<bool>();
        public BindableProperty<bool> isBackStabbed = new BindableProperty<bool>();
        public BindableProperty<bool> isDead = new BindableProperty<bool>();
        public BindableProperty<State> state = new BindableProperty<State>();
    }
}