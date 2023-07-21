﻿using Stats;
using Utilities;

namespace Skills.Modificators
{
    public class DamageCaster : Modificator
    {
        private readonly int _power;

        public DamageCaster(ISkillCaster caster, TargetUnitRelation targetUnitRelation, int power)
            : base(caster, targetUnitRelation)
        {
            _power = power;
        }

        protected override void ApplyChanges(IStats target)
        {
            var amount = -ValueUtility.CalculatePercent(Caster.Characteristics.Power, _power);
            Caster.Characteristics.ChangeStat(StatAttribute.Health, amount);
        }
    }
}
