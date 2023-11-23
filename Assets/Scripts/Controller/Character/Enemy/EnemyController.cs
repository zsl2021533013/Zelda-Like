using System;
using System.Collections.Generic;
using Behaviour_Tree.Node.Runtime.Core;
using Behaviour_Tree.Runtime;
using Behaviour_Tree.Runtime.Processor;
using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using Sirenix.OdinInspector;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Controller.Character.Enemy
{
    public class EnemyController : MonoBehaviour, IController
    {
        [BoxGroup("Behaviour Tree")] public BehaviourTreeGraph graph;

        [BoxGroup("Config")] public EnemyConfig config;
    
        [BoxGroup("Components")] public Animator animator;
        [BoxGroup("Components")] public NavMeshAgent agent;
        
        private BehaviourTreeProcess _process;
        
        private void Awake()
        {
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.angularSpeed = 1000f;
            
            var model = this.GetModel<IEnemyModel>();
            model.RegisterEnemy(transform, animator, agent, config);
        
            graph.nodes.ForEach(node =>
            {
                if (node is BehaviourTreeNode treeNode)
                {
                    treeNode.transform = transform;
                    treeNode.OnAwake();
                }
            });
        }
        
        private void OnEnable()
        {
            var model = this.GetModel<IEnemyModel>();
            model.RegisterEnemy(transform, animator, agent, config);
        }
        
        private void OnDisable()
        {
            var model = this.GetModel<IEnemyModel>();
            model.UnregisterEnemy(transform);
        }
    
        private void Start()
        {
            _process ??= new BehaviourTreeProcess(graph);
        }

        private void Update()
        {
            _process.Update();
        }
    
        public void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            var nextPosition = agent.nextPosition;
        
            nextPosition = new Vector3(position.x, nextPosition.y, position.z);
            agent.nextPosition = nextPosition;
            position.y = nextPosition.y;
            transform.position = position;
            
            var rotation = animator.rootRotation;
            transform.rotation = rotation;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, config.attackDist);
            Gizmos.DrawWireSphere(transform.position, config.povDist);
            
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.yellow;
            Gizmos.DrawFrustum(Vector3.up, config.povAngle, config.povDist, 0, 1);
            Gizmos.matrix = Matrix4x4.identity;
        }

        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}
