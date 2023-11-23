using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.Character.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Scriptable Object/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        [FoldoutGroup("Sensor")] public float povAngle;
        [FoldoutGroup("Sensor")] public float povDist;
        
        [FoldoutGroup("Combat")] public float attackDist;
        [FoldoutGroup("Combat")] public float alertDist;
    }
}
