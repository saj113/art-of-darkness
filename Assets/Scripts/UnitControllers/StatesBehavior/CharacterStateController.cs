using Core;
using Core.Animation;
using Core.UnityFramework;
using GUIScripts.Messengers;
using Skills.Weapons;
using UnitControllers.States;
using UnitControllers.TouchControllers;

namespace UnitControllers.StatesBehavior
{
    internal class CharacterStateController : StateController, ICharacterStateController
    {
        private readonly ICharacterWeapon _characterWeapon;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IMovementController _movementController;
        private readonly ITouchController _touchController;
        private readonly ICharacterMovementInfo _characterMovementInfo;
        private readonly ISkillUseFailedMessenger _skillUseFailedMessenger;

        public CharacterStateController(
            IStateAnimationController animationController,
            IUnityUpdateEvents updateEvents,
            ICharacterWeapon characterWeapon,
            IUnitGameObjectController unitGameObjectController,
            IMovementController movementController, 
            ITouchController touchController,
            ICharacterMovementInfo characterMovementInfo,
            ISkillUseFailedMessenger skillUseFailedMessenger,
            ILogger logger) 
            : base(animationController, updateEvents, characterWeapon, logger)
        {
            _characterWeapon = characterWeapon;
            _characterMovementInfo = characterMovementInfo;
            _unitGameObjectController = unitGameObjectController;
            _movementController = movementController;
            _touchController = touchController;
            _skillUseFailedMessenger = skillUseFailedMessenger;
        }

        public override void SetIdleState()
        {
            SetState(StateType.CharacterIdleState);
        }

        public override void SetMoveState()
        {
            SetState(StateType.CharacterMoveState);
        }

        public override void SetDeadState()
        {
            SetState(StateType.CharacterDeadState);
        }

        protected override IState GetStateByType(StateType type)
        {
            switch (type)
            {
                case StateType.CharacterDeadState:
                    return new CharacterDeadState(AnimationController);
                case StateType.CharacterIdleState:
                    return new CharacterIdleState(
                        AnimationController,
                        this,
                        _characterWeapon,
                        _unitGameObjectController,
                        _touchController,
                        _characterMovementInfo,
                        _skillUseFailedMessenger);
                case StateType.CharacterMoveState:
                    return new CharacterMoveState(
                        this,
                        _movementController,
                        _characterMovementInfo,
                        _characterWeapon);
                default:
                    return base.GetStateByType(type);
            }
        }
    }
}
