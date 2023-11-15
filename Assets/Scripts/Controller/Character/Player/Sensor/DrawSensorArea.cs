using UnityEngine;

namespace Controller.Character.Player.Player
{
    public class DrawSensorArea : MonoBehaviour
    {
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}