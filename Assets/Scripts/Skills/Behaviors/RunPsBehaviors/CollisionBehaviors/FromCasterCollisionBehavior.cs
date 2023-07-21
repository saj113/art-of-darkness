using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Skills.Particles;
using Stats;
using Utilities;

namespace Skills.Behaviors.RunPsBehaviors.CollisionBehaviors
{
    public class FromCasterCollisionBehavior : CollisionBehaviorBase
    {
        private readonly ISkillCaster _caster;
        private readonly TargetUnitRelation _targetUnitRelation;

        public FromCasterCollisionBehavior(
            IModificatorsApplier modificatorsApplier,
            IGameObjectInstantiater gameObjectInstantiater,
            SkillParticles collisionParticles,
            bool applyBuffs,
            ISkillCaster caster,
            TargetUnitRelation targetUnitRelation)
            : base(
                modificatorsApplier,
                gameObjectInstantiater,
                collisionParticles,
                applyBuffs)
        {
            _caster = caster;
            _targetUnitRelation = targetUnitRelation;
        }

        protected override bool IsUnitCollisionPossible(IStats target)
        {
            return ConditionUtility.CheckUnitRelationship(
                _caster,
                target,
                _targetUnitRelation);
        }

        protected override bool IsBarrierCollisionPossible(Tag colliderOwnerTag)
        {
            return ConditionUtility.CheckUnitRelationship(
                _caster.Characteristics.Tag,
                colliderOwnerTag,
                _targetUnitRelation);
        }
    }
}
