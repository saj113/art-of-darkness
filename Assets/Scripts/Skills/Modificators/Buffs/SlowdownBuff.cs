using Skills.Parameters.ModificatorParameters;
using Stats;

namespace Skills.Modificators.Buffs
{
    public class SlowdownBuff : Buff<ISlowdownBuffParameters>
    {
        public SlowdownBuff(
            ISkillCaster caster, IStats targetStatsStats, ISlowdownBuffParameters parameters) 
            : base(caster, targetStatsStats, parameters)
        {
        }

        public override bool CanBeApplied()
        {
            return !TargetStats.Characteristics.HasResistToSlowdown;
        }

        protected override void AppliedCore()
        {
            TargetStats.Characteristics.DecreaseStatByPercent(
                StatAttribute.MoveSpeed, Parameters.Power);
        }

        protected override void ResetCore()
        {
            TargetStats.Characteristics.IncreaseStatByPercent(
                StatAttribute.MoveSpeed, Parameters.Power);
        }
    }
}
