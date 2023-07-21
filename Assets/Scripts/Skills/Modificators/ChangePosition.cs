using Core;
using Stats;
using UnityEngine;
using Utilities;

namespace Skills.Modificators
{
    public class ChangePosition : Modificator
    {
        private readonly int _power;
        private readonly Direction _direction;

        public ChangePosition(
            ISkillCaster caster,
            TargetUnitRelation targetUnitRelation,
            int power,
            Direction direction)
            : base(caster, targetUnitRelation)
        {
            _power = power;
            _direction = direction;
        }

        protected override void ApplyChanges(IStats target)
        {
            var distanceInPixels = _power;
            if (target.Characteristics.IsFacingRight)
            {
                distanceInPixels *= _direction == Direction.Back ? -1 : 1;
            }
            else
            {
                distanceInPixels *= _direction == Direction.Forward ? -1 : 1;
            }

            target.GameObjectController.Position = new Vector2(
                target.GameObjectController.Position.x + distanceInPixels, target.GameObjectController.Position.y);
        }
    }
}
