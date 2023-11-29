using Data.Character.Base;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    public enum SpecialAbilityType
    {
        Fireball,
        TimeStop
    }
    
    public class PlayerStatus : ScriptableObject
    {
        public StatusProperty<bool> isParrying = new StatusProperty<bool>();
        public StatusProperty<SpecialAbilityType> specialAbility = new StatusProperty<SpecialAbilityType>();
    }
}