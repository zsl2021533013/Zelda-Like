using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator animator;

    private void OnAnimatorMove()
    {
        transform.position += animator.deltaPosition;
        Debug.Log(animator.deltaPosition);
    }
}