using UnityEngine;

namespace Controller.Character.Player.Player
{
    public partial class PlayerController
    {
        public Animator animator;
        public Rigidbody rb;
        public Camera cam;
        public PlayerConfig config;
        
        private static readonly int SpeedXZParam = Animator.StringToHash("SpeedXZ");
    }
}