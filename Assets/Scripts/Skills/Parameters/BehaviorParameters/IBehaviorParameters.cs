using Skills.Behaviors;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IBehaviorParameters : IModificatorsApplier
    {
        SkillBehaviorType Type { get; }
        AnimationSkillParticles AnimationParticles { get; }
    }
}
