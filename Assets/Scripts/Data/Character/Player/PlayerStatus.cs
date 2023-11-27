using Data.Character.Base;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    public class PlayerStatus : ScriptableObject
    {
        public StatusProperty<bool> isParrying = new StatusProperty<bool>();
    }
}