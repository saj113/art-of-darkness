using System;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace GUIScripts.Elements
{
    [RequireComponent(typeof(EventTrigger))]
    public class MenuButton : MonoBehaviour
    {
        private MenuAudioPlayer _menuAudioPlayer;
        private EventTrigger _eventTrigger;

        void Start()
        {
            _eventTrigger = GetComponent<EventTrigger>();
            _menuAudioPlayer = FinderUtility.GetComponent<MenuAudioPlayer>();
            Contract.Require(_menuAudioPlayer != null, "_menuAudioPlayer == null");
            
            RegisterOnClickCallback(() => _menuAudioPlayer.PlayButtonClicked());
        }

        void OnDestroy()
        {
            _eventTrigger.triggers.Clear();
        }

        public void RegisterOnClickCallback(Action callback)
        {
            var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
            pointerDownEntry.callback.AddListener(p => callback());
            _eventTrigger.triggers.Add(pointerDownEntry);
        }
    }
}
