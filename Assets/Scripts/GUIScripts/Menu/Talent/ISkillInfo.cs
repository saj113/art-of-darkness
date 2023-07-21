using Skills;
using UnityEngine;

namespace GUIScripts.Menu.Talent
{
    public interface ISkillInfo
    {
        string Name { get; }
        string Description { get; }
        TalentNameKey Key {get;}
        ITalentStatus Status { get; }
        Sprite Icon { get; }
    }
}