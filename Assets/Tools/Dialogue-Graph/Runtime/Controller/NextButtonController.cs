using System;
using System.Collections;
using System.Collections.Generic;
using QFramework.Example;
using UnityEngine;
using UnityEngine.Events;

public class NextButtonController : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent onClick = new UnityEvent();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClick?.Invoke();
        }
    }
}
