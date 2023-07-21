using System.Linq;
using Core.Provider;
using Core.UnityFramework;
using Skills.Behaviors.RunPsBehaviors.CollisionBehaviors;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnityEngine;

namespace Skills.Behaviors.RunPsBehaviors
{
    public class ParticlesToCasterFromTargets : SkillBehaviorToPoint<IParticlesToCasterFromTargetsParameters>
    {
        public ParticlesToCasterFromTargets(ISkillCaster caster,
            IParticlesToCasterFromTargetsParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            ITargetUnitProvider targetUnitProvider,
            float targetPosition)
            : base(caster, parameters, gameObjectInstantiater, targetUnitProvider, targetPosition)
        {
        }

        protected override void ActivateCore()
        {
            var particlesCasters = GetUnits(TargetPosition,
                Parameters.TargetUnitRelation,
                Parameters.Distance,
                Parameters.MaxTargetCount);

            foreach (var particles in particlesCasters)
            {
                GameObjectInstantiater.TryInstantiate(Parameters.CollisionParticleses,
                    particles.GameObjectController.CenterPosition);
            }
            
            var particlesToCasterCollisionBehavior = new ParticlesToTargetCollisionBehavior(
                Parameters,
                GameObjectInstantiater,
                Parameters.CollisionParticleses,
                !IsActivated,
                Caster.Characteristics);
            foreach (var particlesCaster in particlesCasters)
            {
                var instance = GameObjectInstantiater.Instantiate(Parameters.MovementSkillParticlesParameters.Particles);
                instance.Initialize(
                    Caster.Characteristics.Tag,
                    new ParticlesTarget(Caster.GameObjectController),
                    particlesToCasterCollisionBehavior,
                    particlesCaster.GameObjectController.CenterPosition);
                Parameters.ApplyModificators(particlesCaster, IsActivated);
            }
        }

        public override bool IsActivatable(out SkillUseFailedReason failedReason)
        {
            var result = GetUnits(TargetPosition,
                    Parameters.TargetUnitRelation,
                    Parameters.Distance,
                    Parameters.MaxTargetCount)
                .Any();
            failedReason = result ? SkillUseFailedReason.None : SkillUseFailedReason.TargetsNotFound;
            return result;
        }
    }
}
