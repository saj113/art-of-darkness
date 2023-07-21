using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Modificators;
using Skills.Particles;
using UnityEngine;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IParticlesFromCasterParameters : IParticlesParameters
    {
        Vector2 DeviationFromCenter { get; }
    }

    public class ParticlesFromCasterParameters : ParticlesParameters, IParticlesFromCasterParameters
    {
        public ParticlesFromCasterParameters(
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            TargetUnitRelation targetUnitRelation,
            IMovementSkillParticlesParameters movementSkillParticlesParameters,
            SkillParticles collisionParticleses,
            Vector2 deviationFromCenter)
            : base(
                SkillBehaviorType.ParticlesFromCaster,
                animationParticles,
                modificators,
                targetUnitRelation,
                movementSkillParticlesParameters,
                collisionParticleses)
        {
            DeviationFromCenter = deviationFromCenter;
        }

        public Vector2 DeviationFromCenter { get; private set; }
    }
}
