using Data.Character.Base;
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
        
        public StatusProperty<bool> isParried = new StatusProperty<bool>();
        public StatusProperty<bool> isStabbed = new StatusProperty<bool>();
        public StatusProperty<bool> isStopped = new StatusProperty<bool>();
        public StatusProperty<bool> isBackStabbed = new StatusProperty<bool>();
        public StatusProperty<bool> isDead = new StatusProperty<bool>();
        public StatusProperty<State> state = new StatusProperty<State>();
    }
}