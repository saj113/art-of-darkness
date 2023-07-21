using Stats;
using Utilities;

namespace Skills.Modificators
{
    public class Heal : Modificator
    {
        private readonly int _power;
        public Heal(ISkillCaster caster, TargetUnitRelation targetUnitRelation, int power)
            : base(caster, targetUnitRelation)
        {
            _power = power;
        }

        protected override void ApplyChanges(IStats target)
        {
            target.Characteristics.ChangeStat(
                StatAttribute.Health, 
                ValueUtility.CalculatePercent(
                    Caster.Characteristics.Power, _power));
        }
    }
}
