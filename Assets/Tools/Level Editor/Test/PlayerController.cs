using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [HideInInspector]
    public UnityEvent onUpdate;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, 24), ForceMode.Impulse);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-10, rb.velocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(10, rb.velocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(10, rb.velocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(10, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        onUpdate?.Invoke();
    }
}
