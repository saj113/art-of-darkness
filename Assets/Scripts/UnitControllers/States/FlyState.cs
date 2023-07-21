using System;
using Core.Animation;

namespace UnitControllers.States
{
    public class FlyState : State
    {
        private readonly IStateAnimationController _animationController;

        public FlyState(IStateAnimationController animationController)
        {
            _animationController = animationController;
        }

        public override StateType Type
        {
            get
            {
                return StateType.FlyState;
            }
        }

        public override void EnableState()
        {
            throw new InvalidOperationException();
        }
    }
}