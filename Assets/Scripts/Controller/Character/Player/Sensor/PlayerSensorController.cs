using System;
using System.Linq;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    public interface ISensorProperty<T>
    {
        public T Value { get; }
        public bool IsDetected { get; }
        public void Detect();
    }

    public class SensorProperty<T> : ISensorProperty<T>
    {
        private Action detect;

        public T Value { get; private set; }

        public bool IsDetected { get; private set; }

        public SensorProperty(Func<T> detectFunc, Func<T, bool> resultSetter)
        {
            detect = () =>
            {
                Value = detectFunc();
                IsDetected = resultSetter(Value);
            };
        }

        public void Detect() => detect();

        public static implicit operator bool(SensorProperty<T> sensorProperty) => sensorProperty.IsDetected;
    }

    public partial class PlayerSensorController : MonoBehaviour, IController
    {
        public SensorProperty<Collider[]> groundSensor;

        public SensorProperty<Collider> backStabSensor;
        
        public SensorProperty<Collider> stabSensor;
        
        private void Start()
        {
            groundSensor = new SensorProperty<Collider[]>(
                () => Physics.OverlapBox(
                    groundSensorTrans.position,
                    groundSensorTrans.localScale / 2f,
                    Quaternion.identity,
                    LayerMask.GetMask("Ground")),
                values => values.Length > 0
            );
            
            stabSensor = new SensorProperty<Collider>(
                () => Physics.OverlapBox(stabSensorTrans.position,
                        stabSensorTrans.localScale / 2f,
                        stabSensorTrans.rotation)
                    .FirstOrDefault(collider => collider.CompareTag("Enemy")), 
                value =>
                {
                    if (value == null)
                    {
                        return false;
                    }

                    var enemy = value.transform;
                    var enemySensorPos = enemy.position + enemy.forward;
                    var player = Physics.OverlapBox(
                            enemySensorPos, 
                            stabSensorTrans.localScale / 2f,
                            enemy.rotation)
                        .FirstOrDefault(collider => collider.CompareTag("Player"));

                    var status = this.GetModel<IEnemyModel>().GetEnemyStatus(enemy);
                    
                    return (status.isParried && player) || (status.isStopped && player);
                }
            );
            
            backStabSensor = new SensorProperty<Collider>(
                () => Physics.OverlapBox(stabSensorTrans.position,
                        stabSensorTrans.localScale / 2f,
                        stabSensorTrans.rotation)
                    .FirstOrDefault(collider => collider.CompareTag("Enemy")), 
                value =>
                {
                    if (value == null)
                    {
                        return false;
                    }

                    var enemy = value.transform;
                    var enemySensorPos = enemy.position - enemy.forward;
                    var player = Physics.OverlapBox(
                            enemySensorPos, 
                            stabSensorTrans.localScale / 2f,
                            enemy.rotation)
                        .FirstOrDefault(collider => collider.CompareTag("Player"));

                    return player;
                }
            );
        }

        private void FixedUpdate()
        {
            groundSensor.Detect();
            
            backStabSensor.Detect();
            
            stabSensor.Detect();

            if (stabSensor)
            {
                Debug.Log("Stab Ready");
            }
            
            if (backStabSensor)
            {
                Debug.Log("Back Stab Ready");
            }
        }
        
        public Transform GetAttackTarget()
        {
            var colliders = Physics.OverlapSphere(transform.position, 10f)
                .Where(col => col.CompareTag("Enemy") || col.name == "Fireball");

            var ans = colliders.FirstOrDefault();

            if (ans == null)
            {
                return null;
            }

            colliders.ForEach(col =>
            {
                if (Vector3.Distance(col.transform.position, transform.position) <
                    Vector3.Distance(ans.transform.position, transform.position))
                {
                    ans = col;
                }
            });

            return ans.transform;
        }

        public Vector3 FocusCameraRaycast()
        {
            var camera = this.GetModel<IPlayerModel>().components.Get<Camera>().transform;
            var ray = new Ray(camera.position, camera.forward);

            var info = Physics.RaycastAll(ray)
                .Where(info => info.collider.transform != this.GetModel<IPlayerModel>().components.Get<Transform>())
                .OrderBy(info => Vector3.Distance(info.point, transform.position))
                .FirstOrDefault();
            
            if (info.collider)
            {
                Debug.Log(info.collider.name);
                return info.point;
            }
            
            return camera.position + 10f * camera.forward;
        }

        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}