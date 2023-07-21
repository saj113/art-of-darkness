using System;
using Core.UnityFramework;
using GUIScripts.Messengers;
using GUIScripts.Triggers;

namespace GUIScripts.Bars
{
    public class SealSkillsBar : SkillBarBase
    {
        private ClickableSkillTrigger[] _skillTriggers;

        void Start()
        {
            Initialize();
            
            _skillTriggers = GetComponentsInChildren<ClickableSkillTrigger>();

            SetupSkillBarCells(
                _skillTriggers,
                new string[0],
                CharacterStats.CharacterWeapon.SealSkillParameters.Length);
        }

        protected override IDisposable CreateSkillBarCell(
            int index,
            IUnityUpdateEvents updateEvents,
            ISkillUseFailedMessenger skillUseFailedMessenger)
        {
            return new ClickableSkillBarCell(
                CharacterStats.CharacterWeapon.SealSkillParameters[index],
                CharacterStats.CharacterWeapon,
                CharacterStats.StateController,
                _skillTriggers[index],
                updateEvents,
                skillUseFailedMessenger);
        }
    }
}