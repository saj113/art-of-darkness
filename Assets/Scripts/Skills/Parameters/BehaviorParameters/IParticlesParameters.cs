using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Modificators;
using Skills.Particles;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IParticlesParameters : IBehaviorParameters
    {
        TargetUnitRelation TargetUnitRelation { get; }
        IMovementSkillParticlesParameters MovementSkillParticlesParameters { get; }
        SkillParticles CollisionParticleses { get; }
    }

    public abstract class ParticlesParameters : BehaviorParameters, IParticlesParameters
    {
        protected ParticlesParameters(
            SkillBehaviorType skillBehaviorType,
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            TargetUnitRelation targetUnitRelation,
            IMovementSkillParticlesParameters movementSkillParticlesParameters,
            SkillParticles collisionParticleses)
            : base(skillBehaviorType, animationParticles, modificators)
        {
            TargetUnitRelation = targetUnitRelation;
            MovementSkillParticlesParameters = movementSkillParticlesParameters;
            CollisionParticleses = collisionParticleses;
        }

        public TargetUnitRelation TargetUnitRelation { get; private set; }
        public IMovementSkillParticlesParameters MovementSkillParticlesParameters { get; private set; }
        public SkillParticles CollisionParticleses { get; private set; }
    }
}
