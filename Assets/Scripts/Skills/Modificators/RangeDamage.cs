using System.Linq;
using Core;
using Core.Provider;
using Core.UnityFramework;
using Stats;
using Utilities;

namespace Skills.Modificators
{
    public class RangeDamage : Modificator
    {
        private readonly int _power;
        private readonly Direction _direction;
        private readonly float _radius;
        private readonly ITargetUnitProvider _targetUnitProvider;

        public RangeDamage(
            ISkillCaster caster, 
            TargetUnitRelation targetUnitRelation, 
            ITargetUnitProvider targetUnitProvider,
            int power,
            Direction direction,
            float radius)
            : base(caster, targetUnitRelation)
        {
            _targetUnitProvider = targetUnitProvider;
            _power = power;
            _direction = direction;
            _radius = radius;
        }

        protected override void ApplyChanges(IStats target)
        {
            foreach (var unit in _targetUnitProvider.Get(target.Characteristics,
                                                         TargetUnitRelation,
                                                         target.GameObjectController.Bounds,
                                                         _direction,
                                                         _radius))
            {
                var amount = -ValueUtility.CalculatePercent(Caster.Characteristics.Power, _power);
                unit.Characteristics.ChangeStat(StatAttribute.Health, amount);
                Caster.AgrController.AddDamagedTarget(unit.Characteristics.UnitId, amount);
            }
        }
    }
}
