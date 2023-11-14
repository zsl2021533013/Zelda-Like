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

        private void Start()
        {
            groundSensor = new SensorProperty<Collider[]>(
                () => Physics.OverlapBox(
                    groundSensorTrans.position, 
                    groundSensorTrans.localScale,
                    Quaternion.identity,
                    LayerMask.GetMask("Ground")),
                values => values.Length > 0
                );
        }

        private void FixedUpdate()
        {
            groundSensor.Detect();
        }
    }
}