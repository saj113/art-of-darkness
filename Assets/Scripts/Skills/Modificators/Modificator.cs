using Stats;
using Utilities;

namespace Skills.Modificators
{
    public abstract class Modificator : IModificator
    {
        protected Modificator(ISkillCaster caster, TargetUnitRelation targetUnitRelation)
        {
            Caster = caster;
            TargetUnitRelation = targetUnitRelation;
        }

        public virtual bool IsBuff 
        {
            get { return false; }
        }

        protected TargetUnitRelation TargetUnitRelation { get; private set; }

        protected ISkillCaster Caster { get; private set; }

        public void Apply(IStats target)
        {
            if (!IsTargetActual(target.Characteristics.Tag))
            {
                return;
            }

            ApplyChanges(target);
        }

        protected abstract void ApplyChanges(IStats target);

        protected bool IsTargetActual(Tag targetTag)
        {
            return ConditionUtility.CheckUnitRelationship(
                Caster.Characteristics.Tag, targetTag, TargetUnitRelation);
        }
    }
}
