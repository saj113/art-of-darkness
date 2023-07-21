using Skills.Parameters.ModificatorParameters;
using Stats;
using UnityEngine;
using Utilities;

namespace Skills.Modificators.Buffs
{
    public class HoTBuff : Buff<IRegenBuffParameters>
    {
        private float _elapsedTimeSinceActivation;
        private readonly int _regenPerInterval;
        public HoTBuff(
            ISkillCaster caster, IStats targetStats, IRegenBuffParameters parameters)
            : base(caster, targetStats, parameters)
        {
            var ticks = (int)parameters.Duration / (parameters.Interval > 0 ? parameters.Interval : 1);
            _regenPerInterval = ValueUtility.CalculatePercent(
                                     Caster.Characteristics.MaxHealth, parameters.Percent) / ticks;
        }

        public override void UpdateBuff()
        {
            _elapsedTimeSinceActivation += Time.deltaTime;
            if (_elapsedTimeSinceActivation > Parameters.Interval)
            {
                TargetStats.Characteristics.ChangeStat(
                    StatAttribute.Health, _regenPerInterval);
                _elapsedTimeSinceActivation = 0;
            }
        }
    }
}
