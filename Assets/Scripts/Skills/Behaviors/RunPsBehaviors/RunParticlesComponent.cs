using Core.UnityFramework;
using Skills.Behaviors.RunPsBehaviors.CollisionBehaviors;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnityEngine;

namespace Skills.Behaviors.RunPsBehaviors
{
    public abstract class RunParticlesBehavior<T> : SkillBehavior<T>
        where T : IParticlesParameters
    {
        protected RunParticlesBehavior(ISkillCaster caster, T parameters,
            IGameObjectInstantiater gameObjectInstantiater) : base(caster, parameters, gameObjectInstantiater)
        {
        }

        protected void ExecuteSkillModificators(IStats target)
        {
            Parameters.ApplyModificators(target, IsActivated);
        }

        protected void StartMovementParticles(
            Vector2 startPosition, 
            ParticlesTarget particlesTarget,
            ICollisionBehavior collisionBehavior)
        {
            for (var i = 0; i < Parameters.MovementSkillParticlesParameters.ParticlesInstancesCount;  i++)
            {
                var instance = GameObjectInstantiater.Instantiate(Parameters.MovementSkillParticlesParameters.Particles);
                instance.Initialize(
                    Caster.Characteristics.Tag,
                    particlesTarget,
                    collisionBehavior,
                    startPosition);
            }
        }
    }
}
