using Skills.Parameters.ModificatorParameters;
using Stats;

namespace Skills.Modificators.Buffs
{
    public class ChangeTagBuff : Buff<IChangeTagBuffParameters>
    {
        private readonly Tag _targetTag;
        public ChangeTagBuff(
            ISkillCaster caster, IStats targetStatsStats, IChangeTagBuffParameters parameters) 
            : base(caster, targetStatsStats, parameters)
        {
            _targetTag = TargetStats.Characteristics.Tag;
        }

        protected override void AppliedCore()
        {
            TargetStats.Characteristics.Tag = Parameters.Tag;
        }

        protected override void ResetCore()
        {
            if (TargetStats.Characteristics.Tag == Parameters.Tag)
            {
                TargetStats.Characteristics.Tag = _targetTag;
            }
        }
    }
}
