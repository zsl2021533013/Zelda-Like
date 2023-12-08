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

    public Dictionary<Transform, Func<bool>> interactableTriggers = new Dictionary<Transform, Func<bool>>();

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

    public void RegisterInteractableTrigger(Transform trigger, Func<bool> condition)
    {
        interactableTriggers.Add(trigger, condition);
        
        Debug.Log("Add Trigger");
    }

    public void UnregisterInteractableTrigger(Transform trigger)
    {
        interactableTriggers.Remove(trigger);
    }
}
