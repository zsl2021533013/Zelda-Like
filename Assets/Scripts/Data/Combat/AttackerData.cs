using UnityEngine;

namespace Data.Combat
{
    [CreateAssetMenu(fileName = "Attacker Data", menuName = "Scriptable Object/Combat/Attacker Data")]
    public class AttackerData : ScriptableObject
    {
        public int attack;
    }
}