using System;
using System.Linq;
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

    public partial class PlayerSensorController : MonoBehaviour
    {
        public SensorProperty<Collider[]> groundSensor;

        public SensorProperty<Collider> backStabSensor;
        
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
            
            backStabSensor = new SensorProperty<Collider>(
                () => Physics.OverlapBox(backStabSensorTrans.position,
                        backStabSensorTrans.localScale / 2f,
                        backStabSensorTrans.rotation)
                    .FirstOrDefault(collider => collider.CompareTag("Enemy")), 
                value =>
                {
                    if (value == null)
                    {
                        return false;
                    }

                    var enemy = value.transform;
                    var enemySensorPos = enemy.position - backStabSensorTrans.localPosition;
                    var player = Physics.OverlapBox(
                            enemySensorPos, 
                            backStabSensorTrans.localScale / 2f,
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

            if (backStabSensor)
            {
                Debug.Log("Stab Ready");
            }
        }
    }
}