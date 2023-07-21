using Core.Animation;

namespace UnitControllers.States
{
    internal class StunState : State
    {
        private readonly IStateAnimationController _animationController;

        public StunState(IStateAnimationController animationController)
        {
            _animationController = animationController;
        }

        public override StateType Type
        {
            get { return StateType.StunState; }
        }

        public override void EnableState()
        {
            _animationController.SetStunAnimation();
        }
    }
}
