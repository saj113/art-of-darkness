using Skills.Parameters.ModificatorParameters;
using Stats;
using UnityEngine;
using Utilities;

namespace Skills.Modificators.Buffs
{
    /// <summary>
    /// SkillBehavior implementing DamageOverTime
    /// </summary>
    public class DoTBuff : Buff<IDoTBuffParameters>
    {
        private readonly int _interval;
        private readonly int _damegePerInterval;
        private float _elapsedTimeSinceActivation;
        public DoTBuff(
            ISkillCaster caster, IStats targetStats, IDoTBuffParameters parameters)
            : base(caster, targetStats, parameters)
        {
            _interval = parameters.Interval;
            var takts = (int)parameters.Duration / (parameters.Interval > 0 ? parameters.Interval : 1);
            _damegePerInterval = ValueUtility.CalculatePercent(
                Caster.Characteristics.Power, parameters.Power) / takts;
        }

        public override void UpdateBuff()
        {
            _elapsedTimeSinceActivation += Time.deltaTime;
            if (_elapsedTimeSinceActivation > _interval)
            {
                TargetStats.Characteristics.ChangeStat(
                    StatAttribute.Health, -_damegePerInterval);
                _elapsedTimeSinceActivation = 0;
                Caster.AgrController.AddDamagedTarget(TargetStats.Characteristics.UnitId, -_damegePerInterval);
            }
        }
    }
}
