using System;
using System.Linq;
using Core;
using Core.UnityFramework;
using GUIScripts.Triggers;
using Skills;
using Skills.Cooldown;
using Skills.Parameters;
using UnityEngine;
using UnityEngine.UI;

namespace GUIScripts.Bars
{
    public class SkillBarCell<T> : IDisposable where T : ISkillParameters
    {
        private readonly T _skillParameters;
        private readonly ISkillTrigger _skillTrigger;
        private readonly IUnityUpdateEvents _unityUpdateEvents;
        private readonly ITimeCooldown _timeCooldown;
        private readonly ISkillCooldown _manaCooldown;

        public SkillBarCell(
            T skillParameters,
            ISkillTrigger skillTrigger, 
            IUnityUpdateEvents unityUpdateEvents)
        {
            _unityUpdateEvents = unityUpdateEvents;
            _skillParameters = skillParameters;
            _skillTrigger = skillTrigger;
            _skillTrigger.SetSprite(_skillParameters.General.Icon);
            
            _timeCooldown = skillParameters.General.SkillCooldownCollection
                .SingleOrDefault(p => p.Type == SkillCooldownType.Time) as ITimeCooldown;
            _manaCooldown = skillParameters.General.SkillCooldownCollection
                .SingleOrDefault(p => p.Type == SkillCooldownType.Mana);
            
            if (_timeCooldown != null)
            {
                _unityUpdateEvents.FixedUpdateFired += UpdateStateByCoolDown;
            } 
            
            if (_manaCooldown != null)
            {
                _unityUpdateEvents.FixedUpdateFired += UpdateStateByMana;
            }
        }

        protected T SkillParameters
        {
            get { return _skillParameters; }
        }

        private void UpdateStateByCoolDown(float deltaTime)
        {
            _skillTrigger.UpdateStateByCooldown(_timeCooldown.CurrentCooldown, _timeCooldown.RequiredCooldown);
        }

        private void UpdateStateByMana(float amount)
        {
            _skillTrigger.UpdateStateByMana(_manaCooldown.IsReady() ? 0 : 1);
        }

        public virtual void Dispose()
        {
            _unityUpdateEvents.FixedUpdateFired -= UpdateStateByCoolDown;
            _unityUpdateEvents.FixedUpdateFired -= UpdateStateByMana;
        }
    }
}
