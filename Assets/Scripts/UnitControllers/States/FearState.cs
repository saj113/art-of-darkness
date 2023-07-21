using Core.Animation;

namespace UnitControllers.States
{
    internal class FearState : State
    {
        private readonly IStateAnimationController _animationController;

        public FearState(IStateAnimationController animationController)
        {
            _animationController = animationController;
        }

        public override StateType Type
        {
            get { return StateType.FearState; }
        }

        public override void EnableState()
        {
            _animationController.SetRunAnimation();
        }
    }
}
