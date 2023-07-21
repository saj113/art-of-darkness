using Stats;
using Utilities;

namespace Skills.Modificators
{
    public class DamageByPercent : Modificator
    {
        private readonly int _power;

        public DamageByPercent(ISkillCaster caster, int power)
            : this(caster, TargetUnitRelation.Enemy, power)
        {
        }

        public DamageByPercent(ISkillCaster caster, TargetUnitRelation targetUnitRelation, int power)
            : base(caster, targetUnitRelation)
        {
            _power = power;
        }

        protected override void ApplyChanges(IStats target)
        {
            var amount = -ValueUtility.CalculatePercent(target.Characteristics.MaxHealth, _power);
            target.Characteristics.ChangeStat(StatAttribute.Health, amount);
            Caster.AgrController.AddDamagedTarget(target.Characteristics.UnitId, amount);
        }
    }
}