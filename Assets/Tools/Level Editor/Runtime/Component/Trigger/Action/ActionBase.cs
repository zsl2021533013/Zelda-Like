﻿using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public enum ActionState
    {
        Pending,
        Perform,
        Finish
    }
    
    public interface IAction
    {
        public ActionState State { get; }
        public void OnEnter();
        public void OnUpdate();
        public void OnExit();
        public bool CanExit();
        public void StartAction(TriggerController controller);
    }
    
    [Serializable]
    public abstract class ActionBase : IAction
    {
        public ActionState State { get; private set; } = ActionState.Pending;

        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual bool CanExit()
        {
            return true;
        }

        public void StartAction(TriggerController controller)
        {
            OnEnter();

            State = ActionState.Perform;
            
            var observable = Observable.EveryUpdate().First(_ => CanExit());
            
            Observable.EveryUpdate()
                .TakeUntil(observable)
                .Subscribe(_ =>
                {
                    OnUpdate();
                }).AddTo(controller);
                
            observable.Subscribe(_ => 
            { 
                State = ActionState.Finish;
                OnExit(); 
                controller.TryFinishTrigger();
            }).AddTo(controller);
        }
    }
}