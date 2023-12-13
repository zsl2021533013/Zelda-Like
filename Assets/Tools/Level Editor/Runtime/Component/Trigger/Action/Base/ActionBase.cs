using System;
using QFramework;
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
        public void OnEnable();
        public void OnEnter();
        public void OnUpdate();
        public void OnExit();
        public bool CanExit();
        public void StartAction(TriggerController controller);
    }
    
    [Serializable]
    public abstract class ActionBase : IAction, IController
    {
        [HideInInspector]
        public TriggerController controller;
        
        public ActionState State { get; private set; } = ActionState.Pending;

        public virtual void OnEnable() { }
        
        public virtual void OnDisable() { }

        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }
        
        public virtual void OnFixedUpdate() { }

        public virtual void OnExit() { }

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
            
            Observable.EveryFixedUpdate()
                .TakeUntil(observable)
                .Subscribe(_ =>
                {
                    OnFixedUpdate();
                }).AddTo(controller);
                
            observable.Subscribe(_ => 
            { 
                State = ActionState.Finish;
                OnExit(); 
                controller.TryFinishTrigger();
            }).AddTo(controller);
        }

        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}