using Core;
using Skills.Behaviors;
using Skills.Modificators;
using Utilities;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IRayFromCasterParameters : IBehaviorParameters
    {
        TargetUnitRelation TargetUnitRelation { get; }
        float Distance { get; }
        Direction Direction { get; }
        int MaxTargetCount { get; }
    }

    public class RayFromCasterParameters : BehaviorParameters, IRayFromCasterParameters
    {
        public RayFromCasterParameters(AnimationSkillParticles animationParticles, IModificator[] modificators, TargetUnitRelation targetUnitRelation, float distance, Direction direction, int maxTargetCount)
            : base(SkillBehaviorType.RayFromCaster, animationParticles, modificators)
        {
            TargetUnitRelation = targetUnitRelation;
            Distance = distance;
            Direction = direction;
            MaxTargetCount = maxTargetCount;
        }

        public TargetUnitRelation TargetUnitRelation { get; private set; }
        public float Distance { get; private set; }
        public Direction Direction { get; private set; }
        public int MaxTargetCount { get; private set; }
    }
}
