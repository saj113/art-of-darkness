using Core.UnityFramework;
using Skills.Behaviors.RunPsBehaviors.CollisionBehaviors;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnityEngine;
using Utilities;

namespace Skills.Behaviors.RunPsBehaviors
{
    public class ParticlesFromCaster : RunParticlesBehavior<IParticlesFromCasterParameters>
    {
        public ParticlesFromCaster(ISkillCaster caster, IParticlesFromCasterParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater) : base(caster, parameters, gameObjectInstantiater)
        {
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
            var direction = ValueUtility.GetDirection(Caster.Characteristics);
            var endPoingX = (Caster.GameObjectController.Position.x + Constants.ParticlesMaxDistance) * direction;
            var vector = new Vector2(
                Caster.GameObjectController.CenterPosition.x + endPoingX, 
                Caster.GameObjectController.CenterPosition.y);

            return new ParticlesTarget(vector);
        }

        protected Vector2 GetStartPosition()
        {
            var center = Caster.GameObjectController.CenterPosition;
            var direction = ValueUtility.GetDirection(Caster.Characteristics);
            var particlesPosition = new Vector2(
                center.x + Parameters.DeviationFromCenter.x * direction,
                center.y + Parameters.DeviationFromCenter.y);

            return particlesPosition;
        }
    }
}
