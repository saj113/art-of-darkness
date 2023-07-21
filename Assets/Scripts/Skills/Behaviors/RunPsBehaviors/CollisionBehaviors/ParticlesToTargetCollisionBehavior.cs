using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Skills.Particles;
using Stats;

namespace Skills.Behaviors.RunPsBehaviors.CollisionBehaviors
{
    public class ParticlesToTargetCollisionBehavior : CollisionBehaviorBase
    {
        private readonly ICharacteristics _target;

        public ParticlesToTargetCollisionBehavior(
            IModificatorsApplier modificatorsApplier,
            IGameObjectInstantiater gameObjectInstantiater,
            SkillParticles collisionParticles,
            bool applyBuffs,
            ICharacteristics target)
            : base(
                modificatorsApplier,
                gameObjectInstantiater,
                collisionParticles,
                applyBuffs)
        {
            _target = target;
        }

        protected override bool IsUnitCollisionPossible(IStats target)
        {
            return _target.Equals(target.Characteristics);
        }

        protected override bool IsBarrierCollisionPossible(Tag colliderOwnerTag)
        {
            return _target.Tag == colliderOwnerTag;
        }
    }
}
