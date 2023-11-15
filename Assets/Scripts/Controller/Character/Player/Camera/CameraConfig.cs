using UnityEngine;

namespace Controller.Character.Player.Player
{
    [CreateAssetMenu(fileName = "Camera Config", menuName = "Scriptable Object/Camera Config")]
    public class CameraConfig : ScriptableObject
    {
        public float cameraSpeedX;
        public float cameraSpeedY;
        
        public float topClamp;
        public float bottomClamp;
    }
}