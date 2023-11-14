using UnityEngine;

namespace Controller.Character.Player.Player
{
    public partial class PlayerController
    {
        public Animator animator;
        public Rigidbody rb;
        public Collider moveCollider;
        public Camera cam;
        public PlayerSensorController sensorController;
        public PlayerConfig config;
        
        private static readonly int SpeedXZParam = Animator.StringToHash("SpeedXZ");
    }
}