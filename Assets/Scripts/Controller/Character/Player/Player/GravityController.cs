using UnityEngine;

namespace Controller.Character.Player.Player
{
    public class GravityController : MonoBehaviour
    {
        public float gravityScale;
        public Rigidbody rb;

        private void Awake ()
        {
            rb.useGravity = false;
        }

        private void FixedUpdate ()
        {
            var gravity = 9.8f * gravityScale * Vector3.down;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
