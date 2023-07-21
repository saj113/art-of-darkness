using System.Linq;
using Core;
using Core.Provider;
using Core.UnityFramework;
using Skills.Behaviors.RunPsBehaviors.CollisionBehaviors;
using Skills.Parameters.BehaviorParameters;
using Stats;
using Utilities;

namespace Skills.Behaviors.RunPsBehaviors
{
    public class ParticlesToCaster : RunParticlesBehavior<IParticlesToCasterParameters>
    {
        private readonly ITargetUnitProvider _targetUnitProvider;
        public ParticlesToCaster(
            ISkillCaster caster,
            IParticlesToCasterParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            ITargetUnitProvider targetUnitProvider)
            : base(caster, parameters, gameObjectInstantiater)
        {
            _targetUnitProvider = targetUnitProvider;
        }

        protected override void ActivateCore()
        {
            var units = _targetUnitProvider.Get(Caster.Characteristics,
                                                Parameters.TargetUnitRelation,
                                                Caster.GameObjectController.Bounds,
                                                Direction.Round,
                                                Parameters.Distance)
                                           .Take(Parameters.MaxTargetCount)
                                           .ToArray();

            foreach (var unit in units)
            {
                GameObjectInstantiater.TryInstantiate(Parameters.CollisionParticleses,
                                                      unit.GameObjectController.CenterPosition);
            }

            var particlesToCasterCollisionBehavior = new ParticlesToTargetCollisionBehavior(
                Parameters,
                GameObjectInstantiater,
                Parameters.CollisionParticleses,
                !IsActivated,
                Caster.Characteristics);
            foreach (var unit in units)
            {
                StartMovementParticles(
                    unit.GameObjectController.CenterPosition,
                    new ParticlesTarget(Caster.GameObjectController),
                    particlesToCasterCollisionBehavior);

                ExecuteSkillModificators(unit);
            }
        }
    }
}
