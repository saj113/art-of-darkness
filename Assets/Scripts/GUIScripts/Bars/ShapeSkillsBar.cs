using System;
using Core.UnityFramework;
using GUIScripts.Messengers;
using GUIScripts.Triggers;
using Skills.Parameters;

namespace GUIScripts.Bars
{
    public class ShapeSkillsBar : SkillBarBase
    {
        private SkillTrigger[] _skillTriggers;
        void Start()
        {
            _skillTriggers = GetComponentsInChildren<SkillTrigger>();
            Initialize();

            SetupSkillBarCells(
                _skillTriggers,
   new string[0],
                CharacterStats.CharacterWeapon.ShapeSkillParameters.Length);
        }

        protected override IDisposable CreateSkillBarCell(
            int index,
            IUnityUpdateEvents updateEvents,
            ISkillUseFailedMessenger skillUseFailedMessenger)
        {
            return new SkillBarCell<ISkillParameters>(
                CharacterStats.CharacterWeapon.ShapeSkillParameters[index],
                _skillTriggers[index],
                updateEvents);
        }
    }
}
