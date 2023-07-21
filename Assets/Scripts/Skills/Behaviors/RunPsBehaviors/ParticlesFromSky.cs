using Core.UnityFramework;
using Skills.Behaviors.RunPsBehaviors.CollisionBehaviors;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnityEngine;

namespace Skills.Behaviors.RunPsBehaviors
{
    public class ParticlesFromSky : RunParticlesBehavior<IParticlesParameters>
    {
        private readonly float _targetPosition;
        public ParticlesFromSky(ISkillCaster caster, IParticlesParameters parameters, IGameObjectInstantiater gameObjectInstantiater, float targetPosition)
         : base(caster, parameters, gameObjectInstantiater)
        {
            _targetPosition = targetPosition;
        }

        protected override void ActivateCore()
        {
            var fromCasterCollisionBehavior = new FromCasterCollisionBehavior(
                Parameters,
                GameObjectInstantiater,
                Parameters.CollisionParticleses,
                !IsActivated,
                Caster, 
                Parameters.TargetUnitRelation);

            StartMovementParticles(GetStartPosition(), GetTarget(), fromCasterCollisionBehavior);
        }

        protected ParticlesTarget GetTarget()
        {
            return new ParticlesTarget(new Vector2(_targetPosition, -1));
        }

        protected Vector2 GetStartPosition()
        {
            var particlesPosition = new Vector2(
                _targetPosition, 30);

            return particlesPosition;
        }
    }
}