using Skills.Weapons;
using UnityEngine;

namespace UnitControllers.States
{
    internal class CharacterMoveState : State
    {
        private readonly IStateController _stateController;
        private readonly IMovementController _movementController;
        private readonly ICharacterMovementInfo _characterMovementInfo;
        private readonly ICharacterWeapon _characterWeapon;

        public CharacterMoveState(
            IStateController stateController, 
            IMovementController movementController,
            ICharacterMovementInfo characterMovementInfo,
            ICharacterWeapon characterWeapon)
        {
            _stateController = stateController;
            _movementController = movementController;
            _characterMovementInfo = characterMovementInfo;
            _characterWeapon = characterWeapon;
        }

        public override StateType Type
        {
            get { return StateType.CharacterMoveState; }
        }

        public override void EnableState()
        {
            _characterWeapon.SkillAnimationStarted += OnSkillAnimationStarted;
        }

        public override void ResetState()
        {
            _movementController.Stop();
            _characterMovementInfo.Dispose();
            _characterWeapon.SkillAnimationStarted -= OnSkillAnimationStarted;
        }

        public override void Update()
        {
            _movementController.MoveWithVelocity(_characterMovementInfo.GetMoveDirection());
            
            if (!_characterMovementInfo.IsMoving())
            {
                _stateController.SetIdleState();
            }
        }
        
        private void OnSkillAnimationStarted()
        {
            _stateController.SetAttackState();
        }
    }
}
