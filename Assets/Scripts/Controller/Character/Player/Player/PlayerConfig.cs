using UnityEngine;

namespace Controller.Character.Player.Player
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Scriptable Object/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public float walkSpeed;
        public float runSpeed;
    }
}