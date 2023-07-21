using Core;
using Core.Animation;
using Core.UnityFramework;
using Skills.Weapons;
using Stats;
using UnitControllers.AcolytesBehavior;
using UnitControllers.DetectionTargets;
using UnitControllers.States;

namespace UnitControllers.StatesBehavior
{
    internal class UnitStateController : StateController, IUnitStateController
    {
        private readonly IUnitWeapon _unitWeapon;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IMovementController _movementController;
        private readonly IPriorityTargetProvider _priorityTargetProvider;
        private readonly ICharacteristics _characteristics;
        private readonly IFollowingController _followingController;

        public UnitStateController(
            IStateAnimationController animationController, 
            IUnityUpdateEvents updateEvents, 
            IUnitWeapon unitWeapon, 
            IUnitGameObjectController unitGameObjectController, 
            IMovementController movementController, 
            IPriorityTargetProvider priorityTargetProvider,
            ICharacteristics characteristics, 
            IFollowingController followingController,
            ILogger logger) 
            : base(animationController, updateEvents, unitWeapon, logger)
        {
            _unitWeapon = unitWeapon;
            _unitGameObjectController = unitGameObjectController;
            _movementController = movementController;
            _priorityTargetProvider = priorityTargetProvider;
            _characteristics = characteristics;
            _followingController = followingController;
        }

        public override void SetIdleState()
        {
            SetState(StateType.UnitIdleState);
        }

        public override void SetMoveState()
        {
            SetIdleState();
        }

        public override void SetDeadState()
        {
            SetState(StateType.UnitDeadState);
        }

        public void SetUnitFollowState()
        {
            SetState(StateType.UnitFollowState);
        }

        protected override IState GetStateByType(StateType type)
        {
            switch (type)
            {
                case StateType.UnitDeadState:
                    return new UnitDeadState(_characteristics, _unitGameObjectController, AnimationController);

                case StateType.UnitFollowState:
                    return new UnitFollowState(
                        _characteristics.UnitId,
                        _unitGameObjectController,
                        this,
                        _movementController,
                        _priorityTargetProvider,
                        _followingController,
                        AnimationController);

                case StateType.UnitIdleState:
                    return new UnitIdleState(this,
                        _priorityTargetProvider,
                        _followingController,
                        AnimationController,
                        _unitGameObjectController,
                        _unitWeapon,
                        _movementController);
                default:
                    return base.GetStateByType(type);
            }
        }
    }
}
