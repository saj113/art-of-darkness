using Skills;
using UnityEngine;

namespace GUIScripts.Menu.Talent
{
    public class SkillInfo : ISkillInfo
    {
        public SkillInfo(TalentNameKey key, Sprite icon, ITalentStatus status)
        {
            Name = TextProvider.GetTalentName(key);
            Description = TextProvider.GetTalentDescription(key);
            Status = status;
            Icon = icon;
            Key = key;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public Sprite Icon { get; private set; }

        public TalentNameKey Key { get; private set; }

        public ITalentStatus Status { get; private set; }
    }
}