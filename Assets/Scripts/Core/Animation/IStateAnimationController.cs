using System;

namespace Core.Animation
{
    public interface IStateAnimationController : IDisposable
    {
        event Action<StateAnimationType> AnimationCompleted;
        bool IsAnimationRun();
        void SetIdleAnimation();
        void SetDeadAnimation();
        void SetRunAnimation();
        void SetStunAnimation();
        void SetStandUpAnimation();
    }
}