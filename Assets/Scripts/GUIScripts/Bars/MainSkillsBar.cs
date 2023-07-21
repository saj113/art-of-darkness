using System;
using Core.UnityFramework;
using GUIScripts.Messengers;
using GUIScripts.Triggers;

namespace GUIScripts.Bars
{
    public class MainSkillsBar : SkillBarBase
    {
        private ClickableSkillTrigger[] _skillTriggers;

        void Start()
        {
            Initialize();
            
            _skillTriggers = GetComponentsInChildren<ClickableSkillTrigger>();
            var keys = new [] {"z","x","c","v","b","n"};
            
            SetupSkillBarCells(_skillTriggers, keys, CharacterStats.CharacterWeapon.ExtendedSkillParameters.Length);
        }

        protected override IDisposable CreateSkillBarCell(
            int index,
            IUnityUpdateEvents updateEvents,
            ISkillUseFailedMessenger skillUseFailedMessenger)
        {
            return new ClickableSkillBarCell(
                CharacterStats.CharacterWeapon.ExtendedSkillParameters[index],
                CharacterStats.CharacterWeapon,
                CharacterStats.StateController,
                _skillTriggers[index],
                updateEvents,
                skillUseFailedMessenger);
        }
    }
}
