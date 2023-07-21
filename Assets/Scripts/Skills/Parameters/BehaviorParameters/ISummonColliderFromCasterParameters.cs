using Skills.Behaviors;
using Skills.Modificators;

namespace Skills.Parameters.BehaviorParameters
{
    public interface ISummonColliderFromCasterParameters : IBehaviorParameters
    {
        IColliderParameters ColliderParameters { get; }
    }

    public class SummonColliderFromCasterParameters : BehaviorParameters, ISummonColliderFromCasterParameters
    {
        public SummonColliderFromCasterParameters(
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            IColliderParameters colliderParameters)
            : base(SkillBehaviorType.SummonSupportedColliderFromCaster, animationParticles, modificators)
        {
            ColliderParameters = colliderParameters;
        }

        public IColliderParameters ColliderParameters { get; private set; }
    }
}
