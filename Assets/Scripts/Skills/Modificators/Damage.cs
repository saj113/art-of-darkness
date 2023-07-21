using Stats;
using Utilities;

namespace Skills.Modificators
{
    /// <summary>
    /// SkillBehavior implementing dealing damage
    /// </summary>
    public class Damage : Modificator
    {
        private readonly int _power;

        public Damage(ISkillCaster caster, int power)
            : this(caster, TargetUnitRelation.Enemy, power)
        {
        }

        public Damage(ISkillCaster caster, TargetUnitRelation targetUnitRelation, int power) 
            : base(caster, targetUnitRelation)
        {
            _power = power;
        }

        protected override void ApplyChanges(IStats target)
        {
            var amount = -ValueUtility.CalculatePercent(Caster.Characteristics.Power, _power);
            target.Characteristics.ChangeStat(StatAttribute.Health, amount);
            Caster.AgrController.AddDamagedTarget(target.Characteristics.UnitId, amount);
        }
    }
}
