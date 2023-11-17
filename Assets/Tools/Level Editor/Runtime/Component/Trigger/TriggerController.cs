using System;
using System.Collections.Generic;
using System.Linq;
using Level_Editor.Runtime.Action;
using Level_Editor.Runtime.Event;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

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
        public bool hasTriggerAmount;
        
        [FoldoutGroup("Config")]
        [Tooltip("The amount this trigger will work")]
        [ShowIf("hasTriggerAmount", true)]
        public int triggerAmount;

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

        #endregion
        

        private void OnEnable()
        {
            triggerEvents.ForEach(@event =>  @event.Register(this));
        }

        private void OnDisable()
        {
            triggerEvents.ForEach(@event =>  @event.Unregister(this));
        }

        private void OnDrawGizmos()
        {
            var connections = triggerEvents.SelectMany(@event => @event.connections);
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

            if (!hasTriggerAmount)
            {
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
