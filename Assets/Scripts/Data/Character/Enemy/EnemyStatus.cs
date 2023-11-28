using Data.Character.Base;
using UnityEngine;

namespace Data.Character.Enemy
{
    public enum EnemyStatusProperty
    {
        None,
        Parried,
        Stabbed,
        Stopped,
        BackStabbed,
        Dead,
        State_Safe,
        State_Alert,
        State_Combat
    }

    public enum EnemyState
    {
        Safe,
        Alert,
        Combat
    }
    
    public class EnemyStatus : ScriptableObject
    {
        public StatusProperty<bool> isParried = new StatusProperty<bool>();
        public StatusProperty<bool> isStabbed = new StatusProperty<bool>();
        public StatusProperty<bool> isStopped = new StatusProperty<bool>();
        public StatusProperty<bool> isBackStabbed = new StatusProperty<bool>();
        public StatusProperty<bool> isDead = new StatusProperty<bool>();
        public StatusProperty<EnemyState> state = new StatusProperty<EnemyState>();
    }
}