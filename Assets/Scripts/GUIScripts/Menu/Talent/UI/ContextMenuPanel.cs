using System;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUIScripts.Menu.Talent.UI
{
    public class ContextMenuPanel : MonoBehaviour
    {
        [SerializeField]
        private EventTrigger _closeTrigger;

        public event Action Closed;

        private void OnClose()
        {
            var handler = Closed;
            if (handler != null)
            {
                handler();
            }
        }

        void Awake()
        {
            _closeTrigger.Register(EventTriggerType.PointerDown, OnClose);
        }
    }
}
