namespace Skills.Parameters
{
    public interface IExtendedSkillParameters : ISkillParameters
    {
        ISkillParameters HeldSkillParameters { get; }
    }
}
