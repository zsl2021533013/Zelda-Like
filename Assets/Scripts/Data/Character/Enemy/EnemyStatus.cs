using Data.Character.Base;
using UnityEngine;

namespace Data.Character.Enemy
{
    public enum EnemyStatusProperty
    {
        None,
        Stabbed,
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
        public StatusProperty<bool> isBackStabbed = new StatusProperty<bool>();
        public StatusProperty<bool> isStopped = new StatusProperty<bool>();
        public StatusProperty<bool> isDead = new StatusProperty<bool>();
        public StatusProperty<EnemyState> state = new StatusProperty<EnemyState>();
    }
}