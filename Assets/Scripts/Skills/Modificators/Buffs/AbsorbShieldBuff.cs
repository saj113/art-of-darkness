using Core;
using Skills.Parameters.ModificatorParameters;
using Stats;
using UnitControllers.AbsorbingBarrierBehavior;
using Utilities;

namespace Skills.Modificators.Buffs
{
    public class AbsorbShieldBuff : Buff<IAbsorbShieldBuffParameters>
    {
        private readonly AbsorbingBarrier _absorbingBarrier;

        public AbsorbShieldBuff(
            ISkillCaster caster, IStats targetStatsStats, IAbsorbShieldBuffParameters parameters)
            : base(caster, targetStatsStats, parameters)
        {
            var amount = ValueUtility.CalculatePercent(
                TargetStats.Characteristics.MaxHealth, Parameters.AbsorbingHealthPercent);
            _absorbingBarrier = new AbsorbingBarrier(amount);
        }

        public override BuffUniqueCode UniqueCode
        {
            get { return BuffUniqueCode.AbsorbShieldUnique; }
        }

        protected override void AppliedCore()
        {
            TargetStats.AbsorbingBarrierController.AddBarrier(_absorbingBarrier);
        }

        protected override void ResetCore()
        {
            TargetStats.AbsorbingBarrierController.RemoveBarrier(_absorbingBarrier);
        }
    }
}
