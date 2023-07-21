using Skills.Weapons;

namespace UnitControllers.States
{
    internal class AttackState : State
    {
        private readonly IStateController _stateController;
        private readonly IWeapon _weapon;

        public AttackState(
            IStateController stateController, 
            IWeapon weapon)
        {
            _stateController = stateController;
            _weapon = weapon;
        }

        public override StateType Type
        {
            get { return StateType.AttackState; }
        }

        public override void EnableState()
        {
            _weapon.SkillAnimationFinished += AnimationStateOnComplete;
        }

        public override void ResetState()
        {
            _weapon.SkillAnimationFinished -= AnimationStateOnComplete;
        }

        private void AnimationStateOnComplete()
        {
            _stateController.SetIdleState();
        }
    }
}
