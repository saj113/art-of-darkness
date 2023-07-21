using Skills.Behaviors;
using Skills.Modificators;
using Skills.Particles;
using Utilities;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IRayFromPointParameters : IBehaviorParametersToPoint
    {
        TargetUnitRelation TargetUnitRelation { get; }
        float Distance { get; }
        int MaxTargetCount { get; }
    }

    public class RayFromPointParameters : BehaviorParametersToPoint, IRayFromPointParameters
    {
        public RayFromPointParameters(
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            SkillParticles pointParticles,
            TargetUnitRelation targetUnitRelation,
            float distance,
            int maxTargetCount)
            : base(SkillBehaviorType.RayFromPoint, animationParticles, modificators, pointParticles)
        {
            TargetUnitRelation = targetUnitRelation;
            Distance = distance;
            MaxTargetCount = maxTargetCount;
        }

        public TargetUnitRelation TargetUnitRelation { get; private set; }
        public float Distance { get; private set; }
        public int MaxTargetCount { get; private set; }
    }
}
