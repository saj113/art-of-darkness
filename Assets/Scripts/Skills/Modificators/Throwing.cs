using Skills.Parameters.ModificatorParameters;
using Stats;
using UnityEngine;
using Utilities;

namespace Skills.Modificators
{
    public class Throwing : Modificator
    {
        private readonly float _throwingYCoordinate;
        private readonly float _throwingThrust;

        public Throwing(ISkillCaster caster, IModificatorsBuffParameters buffParameters) : this(
            caster,
            buffParameters.TargetRelation,
            buffParameters.ThrowingThrust,
            buffParameters.ThrowingYCoordinate)
        {
        }

        public Throwing(
            ISkillCaster caster, 
            TargetUnitRelation targetUnitRelation,
            float throwingThrust,
            float throwingYCoordinate) : base(caster, targetUnitRelation)
        {
            _throwingThrust = throwingThrust;
            _throwingYCoordinate = throwingYCoordinate;
        }

        protected override void ApplyChanges(IStats target)
        {
//            var currentTargetPosition = target.GameObjectController.CenterPosition;
//            var casterPosition = Caster.GameObjectController.CenterPosition;
//            var direction = ValueUtility.GetDirection(casterPosition, currentTargetPosition);
//
//            target.GameObjectController.AddForce(
//                new Vector2(direction, _throwingYCoordinate) * _throwingThrust);
        }
    }
}
