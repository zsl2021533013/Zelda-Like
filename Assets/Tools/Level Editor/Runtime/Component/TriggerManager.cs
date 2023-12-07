using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;
using UnityEngine.Events;

public class TriggerManager : MonoSingleton<TriggerManager>
{
    #region Callback

    public UnityEvent onUpdate = new UnityEvent();

    #endregion

    public List<Transform> interactableTriggers = new List<Transform>();

    private void Start()
    {
        UIKit.OpenPanel<TriggerPanel>();
    }

    private void Update()
    {
        onUpdate?.Invoke();
    }

    private void OnDisable()
    {
        onUpdate.RemoveAllListeners();
    }

    public void RegisterInteractableTrigger(Transform trigger)
    {
        interactableTriggers.Add(trigger);
        
        Debug.Log("Add Trigger");
    }

    public void UnregisterInteractableTrigger(Transform trigger)
    {
        interactableTriggers.Remove(trigger);
    }
}
