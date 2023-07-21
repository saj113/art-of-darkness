using Skills.Parameters.ModificatorParameters;
using Stats;
using UnitControllers.MovementsBehavior;
using Utilities;

namespace Skills.Modificators.Buffs
{
    public class FearBuff : Buff<IFearBuffParameters>
    {
        private const int Distance = 30;
        private readonly MovementController _unitMovementHelper;

        public FearBuff(
            ISkillCaster caster, IStats targetStatsStats, IFearBuffParameters parameters) :
            base(caster, targetStatsStats, parameters)
        {
            _unitMovementHelper = new MovementController(
                targetStatsStats.GameObjectController,
                targetStatsStats.Characteristics,
                targetStatsStats.StateAnimationController);
        }

        public override void UpdateBuff()
        {
            TargetStats.StateController.SetFearState();

            var directionToCaster = ValueUtility.GetDirection(
                TargetStats.GameObjectController.CenterPosition, Caster.GameObjectController.CenterPosition);
            var goOutX = TargetStats.GameObjectController.CenterPosition.x + (Distance * directionToCaster * -1);

            _unitMovementHelper.MoveToTargetWithDelay(goOutX);
        }

        protected override void ResetCore()
        {
            _unitMovementHelper.Stop();
            TargetStats.StateController.SetIdleState();
        }
    }
}
