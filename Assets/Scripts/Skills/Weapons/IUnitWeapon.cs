using Skills.Parameters;
using Stats;

namespace Skills.Weapons
{
    public interface IUnitWeapon : IWeapon
    {
        ISkillParameters[] SkillParameters { get; }
        bool UseSkill(ISkillParameters skillParameters, IStats target);
        bool IsSkillReady(ISkillParameters skillParameters);
    }
}
