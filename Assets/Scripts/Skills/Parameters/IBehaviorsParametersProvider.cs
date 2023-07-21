using Skills.Parameters.BehaviorParameters;
using Stats;

namespace Skills.Parameters
{
    public interface IBehaviorsParametersProvider
    {
        IBehaviorsParameters GetBehaviorsParameters(ISkillCaster caster);
    }
}
