using Skills.Parameters.ModificatorParameters;
using Stats;
using UnityEngine;

namespace Skills.Modificators.Buffs
{
    public class FlyBuff : Buff<IFlyBuffParameters>
    {
        public FlyBuff(ISkillCaster caster, IStats targetStatsStats, IFlyBuffParameters parameters) 
            : base(caster, targetStatsStats, parameters)
        {
        }

        public override void UpdateBuff()
        {
        }

        protected override void AppliedCore()
        {
            TargetStats.StateController.SetFlyState();
        }

        protected override void ResetCore()
        {

            TargetStats.StateController.SetIdleState();
        }
    }
}