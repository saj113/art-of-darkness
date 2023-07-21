using Skills;
using UnityEngine;

namespace GUIScripts.Menu.Talent {
    public class TalentInfo : SkillInfo, ITalentInfo {
        public TalentInfo (
            TalentNameKey key, Sprite icon,
            ITalentStatus status,
            ISkillInfo extensionLeft1,
            ISkillInfo extensionLeft2,
            ISkillInfo extensionRight1,
            ISkillInfo extensionRight2,
            ISkillInfo bonusExtension)
            : base(key, icon, status)
        {
            ExtensionLeft1 = extensionLeft1;
            ExtensionLeft2 = extensionLeft2;
            ExtensionRight1 = extensionRight1;
            ExtensionRight2 = extensionRight2;
            BonusExtension = bonusExtension;
        }

        public ISkillInfo ExtensionLeft1 { get; private set; }

        public ISkillInfo ExtensionLeft2 { get; private set; }

        public ISkillInfo ExtensionRight1 { get; private set; }

        public ISkillInfo ExtensionRight2 { get; private set; }
        public ISkillInfo BonusExtension { get; private set; }
    }
}