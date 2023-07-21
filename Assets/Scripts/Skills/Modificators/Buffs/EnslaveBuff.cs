using Skills.Parameters.ModificatorParameters;
using Stats;

namespace Skills.Modificators.Buffs
{
    public class EnslaveBuff : Buff<IEnslaveBuffParameters>
    {
        public EnslaveBuff(
            ISkillCaster caster, IStats targetStats, IEnslaveBuffParameters parameters)
            : base(caster, targetStats, parameters)
        {
        }

        public override bool CanBeApplied()
        {
            return !TargetStats.Characteristics.HasResistToStandUp;
        }

        protected override void AppliedCore()
        {
            var newTag = Caster.Characteristics.Tag == Tag.Enemy
                ? Tag.Enemy
                : Tag.Ally;
            TargetStats.Characteristics.Tag = newTag;

            var unit = TargetStats as UnitStats;
            if (unit != null)
            {
                Caster.AcolyteController.AddAcolyte(unit, Parameters.AcolyteType);
                TargetStats.StateController.SetIdleState();
            }
        }

        protected override void ResetCore()
        {
            TargetStats.Characteristics.Kill();
        }
    }
}
