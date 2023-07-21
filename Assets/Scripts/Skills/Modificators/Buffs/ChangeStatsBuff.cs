using Skills.Parameters.ModificatorParameters;
using Stats;

namespace Skills.Modificators.Buffs
{
    public class ChangeStatsBuff : Buff<IChangeStatsBuffParameters>
    {
        public ChangeStatsBuff(
            ISkillCaster caster, IStats targetStats, IChangeStatsBuffParameters parameters)
            : base(caster, targetStats, parameters)
        {
        }

        protected override void AppliedCore()
        {
            TargetStats.Characteristics.IncreaseStatByPercent(
                Parameters.Attribute, Parameters.ChangePercent);
        }

        protected override void ResetCore()
        {
            TargetStats.Characteristics.DecreaseStatByPercent(
                Parameters.Attribute, Parameters.ChangePercent);
        }
    }
}
