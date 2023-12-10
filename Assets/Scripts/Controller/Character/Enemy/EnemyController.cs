using Controller.Combat;
using Model.Interface;
using QFramework;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using Tools.Behaviour_Tree.Runtime.Processor;
using UnityEngine;

namespace Controller.Character.Enemy
{
    public partial class EnemyController : MonoBehaviour, IController, IParried, ITimeStop
    {
        private BehaviourTreeProcess process;
        
        private void OnEnable()
        {
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.angularSpeed = 1000f;
            
            var model = this.GetModel<IEnemyModel>();
            model.RegisterEnemy(transform, this, animator, agent, enemyWeapon, capsuleCollider,config);
            
            runtimeGraph = debugMode ? graph : Instantiate(graph);
            process = new BehaviourTreeProcess(runtimeGraph);
            
            runtimeGraph.nodes.ForEach(node =>
            {
                if (node is BehaviourTreeNode treeNode)
                {
                    treeNode.transform = transform;
                    treeNode.OnEnable();
                }
            });
        }
        
        private void OnDisable()
        {
            var model = this.GetModel<IEnemyModel>();
            model.UnregisterEnemy(transform);
        }

        private void Update()
        {
            if (!enable)
            {
                return;
            }
            
            process.Update();
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, config.attackDist);
            Gizmos.DrawWireSphere(transform.position, config.povDist);
            
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.yellow;
            Gizmos.DrawFrustum(Vector3.up, config.povAngle, config.povDist, 0, 1);
            Gizmos.matrix = Matrix4x4.identity;
            
            Gizmos.DrawWireSphere(transform.position, config.alertDist);
        }

        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
        
        public void Parried()
        {
            var model = this.GetModel<IEnemyModel>();
            var status = model.GetEnemyStatus(transform);
            status.isParried.Value = true;
        }

        public void TimeStop()
        {
            animator.speed = 0f;
            agent.updateRotation = false;
            enable = false;
        }

        public void TimeReset()
        {
            animator.speed = 1f;
            agent.updateRotation = true;
            enable = true;
        }
    }
}
