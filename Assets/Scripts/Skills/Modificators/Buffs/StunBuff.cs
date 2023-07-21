using Skills.Parameters.ModificatorParameters;
using Stats;

namespace Skills.Modificators.Buffs
{
    public class StunBuff : Buff<IStunBuffParameters>
    {
        public StunBuff(
            ISkillCaster caster, IStats targetStats, IStunBuffParameters parameters)
            : base(caster, targetStats, parameters)
        {
        }

        public override bool CanBeApplied()
        {
            return !TargetStats.Characteristics.HasResistToStun;
        }

        public override void UpdateBuff()
        {
            TargetStats.StateController.SetStunState();
        }

        protected override void ResetCore()
        {
            TargetStats.StateController.SetIdleState();
        }
    }
}
