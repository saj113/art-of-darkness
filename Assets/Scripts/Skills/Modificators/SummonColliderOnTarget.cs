using Skills.Parameters;
using Skills.Parameters.ModificatorParameters;
using Stats;

namespace Skills.Modificators
{
    public class SummonColliderOnTarget : Modificator
    {
        private readonly ISkillColliderActivator _colliderActivator;
        private readonly IColliderParameters _colliderParameters;

        public SummonColliderOnTarget(
            ISkillCaster caster,
            ISkillColliderActivator colliderActivator,
            TargetUnitRelation targetUnitRelation,
            IColliderParameters colliderParameters) : base(caster, targetUnitRelation)
        {
            _colliderActivator = colliderActivator;
            _colliderParameters = colliderParameters;
        }

        protected override void ApplyChanges(IStats target)
        {
            _colliderActivator.Activate(
                Caster,
                _colliderParameters,
                target.GameObjectController.CenterPosition.x);
        }
    }
}
