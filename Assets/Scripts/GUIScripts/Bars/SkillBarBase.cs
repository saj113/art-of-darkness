using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.UnityFramework;
using GUIScripts.Messengers;
using GUIScripts.Triggers;
using Stats;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace GUIScripts.Bars
{
    public abstract class SkillBarBase : MonoBehaviour
    {
        private readonly IList<IDisposable> _skillBarCells = new List<IDisposable>();
        private KeyboardEvents _keyboardEvents;
        private readonly IDictionary<string, EventTrigger> _events = new Dictionary<string, EventTrigger>();
        private IUnityUpdateEvents _updateEvents;

        protected abstract IDisposable CreateSkillBarCell(
            int index,
            IUnityUpdateEvents updateEvents,
            ISkillUseFailedMessenger skillUseFailedMessenger);
        
        protected ICharacterStats CharacterStats { get; private set; }

        protected virtual void Dispose()
        {
            foreach (var skillBarCell in _skillBarCells)
            {
                skillBarCell.Dispose();
            }
        }

        protected void Initialize()
        {
            CharacterStats = FinderUtility.GetPlayerStats();
            _updateEvents = InstanceContainer.Instance.Resolve<IUnityUpdateEvents>();
            _keyboardEvents = new KeyboardEvents(_updateEvents);
            _keyboardEvents.KeyDown += OnKeyDown;
            _keyboardEvents.KeyUp += OnKeyUp;
        }
        
        protected void SetupSkillBarCells(SkillTrigger[] skillTriggers, string[] keyboardKeys, int skillsCount)
        {
            var skillUseFailedMessenger = InstanceContainer.Instance.Resolve<ISkillUseFailedMessenger>();
            if (keyboardKeys.Any() && skillsCount > keyboardKeys.Length)
            {
                throw new InvalidOperationException("Keys length is invalid");
            }
            
            for (var i = 0; i <  skillTriggers.Length && i < skillsCount; i++)
            {
                if (keyboardKeys.Any() && skillTriggers[i] is ClickableSkillTrigger clickableSkillTrigger)
                {
                    _events[keyboardKeys[i]] = clickableSkillTrigger.EventTrigger;
                }

                var skillBarCell = CreateSkillBarCell(i, _updateEvents, skillUseFailedMessenger);
                _skillBarCells.Add(skillBarCell);
            }

            if (skillTriggers.Length > skillsCount)
            {
                foreach (var skillTrigger in skillTriggers.Skip(skillsCount))
                {
                    skillTrigger.gameObject.SetActive(false);
                }
            }
        }
        
        private void OnDestroy()
        {
            Dispose();
        }
        
        private void OnKeyDown(string key)
        {
            var value = GetTriggerByKey(key);
            if (value != null)
            {
                value.OnPointerDown(null);
            }
        }

        private void OnKeyUp(string key)
        {
            var value = GetTriggerByKey(key);
            if (value != null)
            {
                value.OnPointerUp(null);
            }
        }

        private EventTrigger GetTriggerByKey(string key)
        {
            EventTrigger value;
            if (!_events.TryGetValue(key, out value))
            {
                return null;
            }

            return value;
        }
    }
}