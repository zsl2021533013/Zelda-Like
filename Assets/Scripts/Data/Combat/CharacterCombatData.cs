using UnityEngine;

namespace Data.Combat
{
    [CreateAssetMenu(fileName = "Character Combat Data", menuName = "Scriptable Object/Combat/Character Combat Data")]
    public class CharacterCombatData : AttackerData
    {
        public int health;
    }
}