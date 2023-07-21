using Skills.Behaviors;
using Skills.Modificators;
using Skills.Particles;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IBehaviorParametersToPoint : IBehaviorParameters
    {
        SkillParticles PointParticles { get; }
    }

    public class BehaviorParametersToPoint : BehaviorParameters, IBehaviorParametersToPoint
    {
        public BehaviorParametersToPoint(
            SkillBehaviorType type,
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            SkillParticles pointParticles)
            : base(type, animationParticles, modificators)
        {
            PointParticles = pointParticles;
        }

        public SkillParticles PointParticles { get; private set; }
    }
}
