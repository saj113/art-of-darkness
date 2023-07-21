using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Modificators;
using Skills.Particles;
using Utilities;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IParticlesToCasterParameters : IParticlesParameters
    {
        float Distance { get; }
        int MaxTargetCount {get;}
    }

    public class ParticlesToCasterParameters : ParticlesParameters, IParticlesToCasterParameters
    {
        public ParticlesToCasterParameters(
            SkillBehaviorType skillBehaviorType,
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            TargetUnitRelation targetUnitRelation,
            IMovementSkillParticlesParameters movementSkillParticlesParameters,
            SkillParticles collisionParticleses,
            float distance,
            int maxTargetCount)
            : base(
                skillBehaviorType,
                animationParticles,
                modificators,
                targetUnitRelation,
                movementSkillParticlesParameters,
                collisionParticleses)
        {
            Distance = distance;
            MaxTargetCount = maxTargetCount;
        }

        public float Distance { get; private set; }
        public int MaxTargetCount { get; private set; }
    }
}
