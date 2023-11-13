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
        Untriggered,
        Triggering,
        Triggered
    }
    
    public class TriggerController : MonoBehaviour
    {
        #region Main Property

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
        
        private TriggerState _state = TriggerState.Untriggered;
        
        public TriggerState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (value == TriggerState.Triggered)
                {
                    onTriggerFinish?.Invoke();
                }
                _state = value;
            }
        }

        #region Callback

        [HideInInspector]
        public UnityEvent onTriggerFinish;

        #endregion
        

        private void OnEnable()
        {
            triggerEvents.ForEach(@event =>  @event.Register(this));
        }

        private void OnDisable()
        {
            triggerEvents.ForEach(@event =>  @event.Unregister(this));
        }

        public void TryTrigger()
        {
            if (State != TriggerState.Untriggered)
            {
                return;
            }
            
            if (triggerConditions.All(condition => condition.Satisfied()))
            {
                triggerActions.ForEach(action => action.Perform(this));
                State = TriggerState.Triggering;
            }
        }
    }
}
