using System;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUIScripts.Triggers
{
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(EventTrigger))]
    public class ClickableSkillTrigger : SkillTrigger, IClickableSkillTrigger
    {
        private EventTrigger _eventTrigger;
        protected override void Awake()
        {
            base.Awake();
            
            _eventTrigger = GetComponent<EventTrigger>();
            
            var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            pointerDownEntry.callback.AddListener(OnPointerDown);
            var pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
            pointerUpEntry.callback.AddListener(OnPointerUp);

            _eventTrigger.triggers.Add(pointerDownEntry);
            _eventTrigger.triggers.Add(pointerUpEntry);
        }

        public event Action PointerDown;
        public event Action PointerUp;
        public EventTrigger EventTrigger => _eventTrigger;

        protected virtual void OnPointerDown(BaseEventData _)
        {
            PointerDown?.Invoke();
        }

        protected virtual void OnPointerUp(BaseEventData _)
        {
            PointerUp?.Invoke();
        }
    }
}