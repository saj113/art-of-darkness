using Core.Animation;

namespace UnitControllers.States
{
    internal class UnitResurrectionState : State
    {
        private readonly IStateController _stateController;
        private readonly IStateAnimationController _animationController;

        public UnitResurrectionState(IStateController stateController, IStateAnimationController animationController)
        {
            _stateController = stateController;
            _animationController = animationController;
        }

        public override StateType Type
        {
            get { return StateType.UnitResurrectionState; }
        }

        public override void EnableState()
        {
            _animationController.AnimationCompleted += AnimationStateOnComplete;
            _animationController.SetStandUpAnimation();
        }

        public override void ResetState()
        {
            _animationController.AnimationCompleted -= AnimationStateOnComplete;
        }

        private void AnimationStateOnComplete(StateAnimationType animationType)
        {
            if (animationType != StateAnimationType.StandUp) {
                return;
            }

            _animationController.AnimationCompleted -= AnimationStateOnComplete;
            _stateController.SetIdleState();
        }
    }
}
