using UnityEngine;

namespace Controller.Character.Player.Player
{
    public class GravityController : MonoBehaviour
    {
        public const float GravityScale = 1.5f;
        
        public Rigidbody rb;

        private void Awake ()
        {
            rb.useGravity = false;
        }

        private void FixedUpdate ()
        {
            var gravity = 9.8f * GravityScale * Vector3.down;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
