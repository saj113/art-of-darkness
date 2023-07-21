using Core.UnityFramework;
using Skills.Behaviors.RunPsBehaviors.CollisionBehaviors;
using Skills.Parameters.BehaviorParameters;
using Stats;

namespace Skills.Behaviors.RunPsBehaviors
{
    public class ParticlesToTarget : ParticlesFromCaster
    {
        private readonly IStats _target;

        public ParticlesToTarget(ISkillCaster caster,
            IParticlesFromCasterParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            IStats target)
            : base(caster, parameters, gameObjectInstantiater)
        {
            _target = target;
        }

        protected override void ActivateCore()
        {
            var particlesToCasterCollisionBehavior = new ParticlesToTargetCollisionBehavior(
                Parameters,
                GameObjectInstantiater,
                Parameters.CollisionParticleses,
                !IsActivated,
                _target.Characteristics);

            StartMovementParticles(
                GetStartPosition(), 
                new ParticlesTarget(_target.GameObjectController), 
                particlesToCasterCollisionBehavior);
        }
    }
}