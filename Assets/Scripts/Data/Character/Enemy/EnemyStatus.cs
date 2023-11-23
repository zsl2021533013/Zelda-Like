using UnityEngine;

namespace Data.Character.Enemy
{
    public interface IStatusProperty<T>
    {
        public void Reset();
    }

    public class StatusProperty<T> : IStatusProperty<T>
    {
        public T Value;

        public void Reset()
        {
            Value = default;
        }

        public static implicit operator T(StatusProperty<T> statusProperty) => statusProperty.Value;
    }
    
    public enum EnemyStatusProperty
    {
        None,
        Stabbed,
        Dead,
        Alert
    }
    
    public class EnemyStatus : ScriptableObject
    {
        public StatusProperty<bool> isStabbed = new StatusProperty<bool>();
        public StatusProperty<bool> isDead = new StatusProperty<bool>();
        public StatusProperty<bool> isAlert = new StatusProperty<bool>();
    }
}