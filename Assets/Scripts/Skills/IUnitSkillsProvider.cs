using Skills.Parameters;

namespace Skills
{
    public interface IUnitSkillsProvider
    {
        ISkillParameters GetSkill(IUnitSkillInfo skillInfo);
    }
}