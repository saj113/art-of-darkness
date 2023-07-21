using Core.Animation;

namespace UnitControllers.States
{
    internal class CharacterDeadState : State
    {
        public override StateType Type
        {
            get { return StateType.CharacterDeadState; }
        }

        private readonly IStateAnimationController _stateAnimationController;

        public CharacterDeadState(IStateAnimationController stateAnimationController)
        {
            _stateAnimationController = stateAnimationController;
        }
        public override void EnableState()
        {
            _stateAnimationController.SetDeadAnimation();
        }
    }
}
