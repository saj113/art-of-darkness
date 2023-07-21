using System;
using Core.Animation;
using UnitControllers.AcolytesBehavior;
using UnitControllers.DetectionTargets;
using Utilities;

namespace UnitControllers.States
{
    internal class UnitFollowState : State
    {
        private readonly Guid _unitId;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IStateController _stateController;
        private readonly IMovementController _movementController;
        private readonly IPriorityTargetProvider _priorityTargetProvider;
        private readonly IFollowingController _followingController;
        private readonly IStateAnimationController _stateAnimationController;
        private float _randomTargetPosition = float.NaN;

        public UnitFollowState(
            Guid unitId,
            IUnitGameObjectController unitGameObjectController, 
            IStateController stateController, 
            IMovementController movementController, 
            IPriorityTargetProvider priorityTargetProvider, 
            IFollowingController followingController,
            IStateAnimationController stateAnimationController)
        {
            _unitId = unitId;
            _unitGameObjectController = unitGameObjectController;
            _stateController = stateController;
            _movementController = movementController;
            _priorityTargetProvider = priorityTargetProvider;
            _followingController = followingController;
            _stateAnimationController = stateAnimationController;
        }

        public override StateType Type
        {
            get { return StateType.UnitFollowState; }
        }

        public override void ResetState()
        {
            _movementController.Stop();
        }

        public override void Update()
        {
            if (_priorityTargetProvider.GetPriorityTarget() != null || 
                !_followingController.IsFolowerValid())
            {
                _stateController.SetIdleState();
                return;
            }

            float fromBound;
            float toBound;
            _followingController.FollowingInstructions.GetFollowBounds(_unitId, out fromBound, out toBound);

            var targetXPosition = GetTargetXPosition(fromBound, toBound);
            if (Math.Abs(_unitGameObjectController.Position.x - targetXPosition) < 0.05f)
            {
                _movementController.Stop();
                return;
            }

            _movementController.MoveToTargetWithDelay(targetXPosition);
        }

        private float GetTargetXPosition(float fromBound, float toBound)
        {
            if (float.IsNaN(_randomTargetPosition) || !IsInBounds(fromBound, toBound))
            {
                _randomTargetPosition = ValueUtility.GetRandom(fromBound, toBound);
            }

            return _randomTargetPosition;
        }

        private bool IsInBounds(float fromBound, float toBound)
        {
            return _unitGameObjectController.Position.x > fromBound && _unitGameObjectController.Position.x < toBound;
        }
    }
}
