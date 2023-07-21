using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Modificators;
using Skills.Particles;
using Utilities;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IParticlesToCasterFromTargetsParameters : IBehaviorParametersToPoint, IParticlesParameters
    {
        float Distance { get; }
        int MaxTargetCount { get; }
    }

    public class ParticlesToCasterFromTargetsParameters : BehaviorParametersToPoint, IParticlesToCasterFromTargetsParameters
    {
        public ParticlesToCasterFromTargetsParameters(
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            SkillParticles pointParticles,
            TargetUnitRelation targetUnitRelation,
            IMovementSkillParticlesParameters movementSkillParticlesParameters,
            SkillParticles collisionParticleses,
            float distance,
            int maxTargetCount)
            : base(SkillBehaviorType.ParticlesToCasterFromTargets, animationParticles, modificators, pointParticles)
        {
            TargetUnitRelation = targetUnitRelation;
            MovementSkillParticlesParameters = movementSkillParticlesParameters;
            CollisionParticleses = collisionParticleses;
            Distance = distance;
            MaxTargetCount = maxTargetCount;
        }

        public TargetUnitRelation TargetUnitRelation { get; private set; }
        public IMovementSkillParticlesParameters MovementSkillParticlesParameters { get; private set; }
        public SkillParticles CollisionParticleses { get; private set; }
        public float Distance { get; private set; }
        public int MaxTargetCount { get; private set; }
    }
}
