using System;
using System.Collections.Generic;
using System.Linq;
using Level_Editor.Runtime.Action;
using Level_Editor.Runtime.Event;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Level_Editor.Runtime
{
    public enum TriggerState
    {
        Pending,
        Perform,
        Finish
    }
    
    public class TriggerController : MonoBehaviour
    {
        #region Main Property

        [FoldoutGroup("Config")]
        [Tooltip("Click if you want limit the trigger times")]
        public bool triggerForever = false;
        
        [FoldoutGroup("Config")]
        [Tooltip("The amount this trigger will work")]
        [HideIf("triggerForever", true)]
        public int triggerAmount = 1;

        [Space(5)]
        [SerializeReference, LabelText("Events")]
        public List<EventBase> triggerEvents = new List<EventBase>();

        [Space(5)]
        [SerializeReference, LabelText("Conditions")]
        public List<ConditionBase> triggerConditions = new List<ConditionBase>();

        [Space(5)]
        [SerializeReference, LabelText("Actions")]
        public List<ActionBase> triggerActions = new List<ActionBase>();

        #endregion
        
        private TriggerState _state = TriggerState.Pending;
        
        public TriggerState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (value == TriggerState.Finish)
                {
                    onTriggerFinish?.Invoke();
                }
                _state = value;
            }
        }

        #region Callback

        [HideInInspector]
        public UnityEvent onTriggerFinish; 
        
        [HideInInspector]
        public UnityEvent onTriggerDisable; 
        
        [HideInInspector]
        public UnityEvent tryTrigger; 

        #endregion
        
        private void OnEnable()
        {
            triggerEvents.ForEach(@event =>  @event.controller = this);
            triggerConditions.ForEach(condition => condition.controller = this);
            triggerActions.ForEach(action =>  action.controller = this);
            
            triggerEvents.ForEach(@event =>  @event.OnEnable());
            triggerConditions.ForEach(condition => condition.OnEnable());
            triggerActions.ForEach(action =>  action.OnEnable());
            
            triggerEvents.ForEach(@event =>  @event.Register(this));
        }

        private void OnDisable()
        {
            triggerEvents.ForEach(@event =>  @event.controller = null);
            triggerConditions.ForEach(condition => condition.controller = null);
            triggerActions.ForEach(action =>  action.controller = null);
            
            triggerEvents.ForEach(@event =>  @event.OnDisable());
            triggerConditions.ForEach(condition => condition.OnDisable());
            triggerActions.ForEach(action =>  action.OnDisable());
            
            triggerEvents.ForEach(@event =>  @event.Unregister(this));
        }

        private void OnDrawGizmos()
        {
            var connections = triggerEvents
                .Where(@event => @event.connections != null)
                .SelectMany(@event => @event.connections)
                .ToList();

            connections.ForEach(connection =>
            {
                if (connection != null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, connection.position);
                }
            });
        }

        public void TryTrigger()
        {
            if (State != TriggerState.Pending)
            {
                return;
            }
            
            if (triggerConditions.All(condition => condition.Satisfied()))
            {
                triggerActions.ForEach(action => action.StartAction(this));
                State = TriggerState.Perform;
            }
        }

        public void TryFinishTrigger()
        {
            if (triggerActions.Any(action => action.State != ActionState.Finish))
            {
                return;
            }
            
            State = TriggerState.Finish;

            if (triggerForever)
            {
                State = TriggerState.Pending;
                return;
            }
            
            triggerAmount--;
            if (triggerAmount > 0)
            {
                State = TriggerState.Pending;
                return;
            }
                    
            Debug.Log($"{transform.name} Trigger Finish");
        }
    }
}
