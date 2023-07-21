using Stats;

namespace Skills.Parameters
{
    public interface IColliderParametersProvider
    {
        IColliderParameters GetColliderParameters(ISkillCaster caster);
    }
}
