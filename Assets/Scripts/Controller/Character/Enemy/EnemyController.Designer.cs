﻿using Behaviour_Tree.Runtime;
using Controller.Combat;
using Data.Character.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.Character.Enemy
{
    public partial class EnemyController
    {
        [Tooltip("Debug mode can show more color result, but can only use for singleton")]
        [BoxGroup("Behaviour Tree")] public bool debugMode;
        [BoxGroup("Behaviour Tree")] public BehaviourTreeGraph graph;
        private BehaviourTreeGraph runtimeGraph;
        
        [BoxGroup("Config")] public EnemyConfig config;
        
        [BoxGroup("Components")] public Animator animator;
        [BoxGroup("Components")] public NavMeshAgent agent;
        [BoxGroup("Components")] public WeaponController weapon;
    }
}