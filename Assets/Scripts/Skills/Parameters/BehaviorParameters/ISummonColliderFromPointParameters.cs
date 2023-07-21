using Skills.Behaviors;
using Skills.Modificators;
using Skills.Particles;

namespace Skills.Parameters.BehaviorParameters
{
    public interface ISummonColliderFromPointParameters : IBehaviorParametersToPoint
    {
        TargetUnitRelation TargetUnitRelation { get; }
        IColliderParameters ColliderParameters { get; }
    }

    public class SummonColliderFromPointParameters : BehaviorParametersToPoint, ISummonColliderFromPointParameters
    {
        public SummonColliderFromPointParameters(
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            SkillParticles pointParticles,
            TargetUnitRelation targetUnitRelation,
            IColliderParameters colliderParameters)
            : base(SkillBehaviorType.SummonColliderFromPoint, animationParticles, modificators, pointParticles)
        {
            TargetUnitRelation = targetUnitRelation;
            ColliderParameters = colliderParameters;
        }

        public TargetUnitRelation TargetUnitRelation { get; private set; }
        public IColliderParameters ColliderParameters { get; private set; }
    }
}
