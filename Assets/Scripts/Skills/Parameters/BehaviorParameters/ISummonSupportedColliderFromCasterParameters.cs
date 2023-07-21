using Skills.Behaviors;
using Skills.Modificators;
using Utilities;

namespace Skills.Parameters.BehaviorParameters
{
    public interface ISummonSupportedColliderFromCasterParameters : IBehaviorParameters
    {
        float Distance { get; }
        IColliderParameters ColliderParameters { get; }
    }

    public class SummonSupportedColliderFromCasterParameters : BehaviorParameters, ISummonSupportedColliderFromCasterParameters
    {
        public SummonSupportedColliderFromCasterParameters(
            SkillBehaviorType type,
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            float distance,
            IColliderParameters colliderParameters)
            : base(type, animationParticles, modificators)
        {
            Distance = distance;
            ColliderParameters = colliderParameters;
        }

        public float Distance { get; private set; }
        public IColliderParameters ColliderParameters { get; private set; }
    }
}