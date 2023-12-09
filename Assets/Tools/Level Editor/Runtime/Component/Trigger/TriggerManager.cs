using System;
using System.Collections;
using System.Collections.Generic;
using Level_Editor.Runtime;
using QFramework;
using QFramework.Example;
using UnityEngine;
using UnityEngine.Events;

public class TriggerManager : MonoSingleton<TriggerManager>
{
    #region Callback

    public UnityEvent onUpdate = new UnityEvent();

    #endregion
    
    public class InteractableTriggerInfo
    {
        public Transform interactPoint;
        public Func<bool> condition;
    }
    
    public Dictionary<TriggerController, InteractableTriggerInfo> interactableTriggers = new ();
    
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

    public void RegisterInteractableTrigger(TriggerController controller, InteractableTriggerInfo info)
    {
        interactableTriggers.Add(controller, info);
        
        Debug.Log("Add Trigger");
    }

    public void UnregisterInteractableTrigger(TriggerController controller)
    {
        interactableTriggers.Remove(controller);
    }
}
