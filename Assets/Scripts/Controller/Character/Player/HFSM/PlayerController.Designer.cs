using Controller.Character.Player.Combat;
using Data.Combat;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    public partial class PlayerController
    {
        public Animator animator;
        public Rigidbody rb;
        public Collider moveCollider;
        [ChildGameObjectsOnly] public PlayerSensorController sensorController;
        [ChildGameObjectsOnly] public PlayerWeaponController weaponController;
        
        private Camera cam;
        private PlayerConfig config;
        private CharacterCombatData combatData;
        
        private static readonly int SpeedXParam = Animator.StringToHash("SpeedX");
        private static readonly int SpeedZParam = Animator.StringToHash("SpeedZ");
        private static readonly int SpeedXZParam = Animator.StringToHash("SpeedXZ");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Boost = Animator.StringToHash("Boost");
    }
}