using Controller.Combat;
using Data.Character.Enemy;
using Sirenix.OdinInspector;
using Tools.Behaviour_Tree.Runtime.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Controller.Character.Enemy
{
    public partial class EnemyController
    {
        [Tooltip("Debug mode can show more color result, but can only use for singleton")]
        [BoxGroup("Controller")] public bool enable = true;
        [BoxGroup("Behaviour Tree")] public bool debugMode;
        [BoxGroup("Behaviour Tree")] public BehaviourTreeGraph graph;
        private BehaviourTreeGraph runtimeGraph;
        
        [BoxGroup("Config")] public EnemyConfig config;
        
        [BoxGroup("Components")] public Animator animator;
        [BoxGroup("Components")] public NavMeshAgent agent;
        [FormerlySerializedAs("weapon")] [BoxGroup("Components"), ChildGameObjectsOnly] public EnemyWeaponController enemyWeapon;
        [BoxGroup("Components")] public CapsuleCollider capsuleCollider;
    }
}