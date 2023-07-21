using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;

namespace Core.Animation
{
    internal sealed class StateAnimationController : AnimationController, IStateAnimationController
    {
        private readonly IStateAnimations _stateAnimations;
        private readonly IDictionary<string, StateAnimationType> _animationsType;

        public event Action<StateAnimationType> AnimationCompleted;

        public StateAnimationController(
            SkeletonAnimation skeletonAnimation, 
            IStateAnimations stateAnimations, 
            ILogger logger) : base(skeletonAnimation, logger)
        {
            _stateAnimations = stateAnimations;
            _animationsType = GetAnimationsTypes(_stateAnimations);
            skeletonAnimation.AnimationState.Complete += OnAnimationCompleted;
        }

        public void SetIdleAnimation()
        {
            SetAnimation(_stateAnimations.Idle, true);
        }

        public void SetDeadAnimation()
        {
            SetAnimation(_stateAnimations.Fall, false);
        }

        public void SetRunAnimation()
        {
            SetAnimation(_stateAnimations.Run, 
                         true, 
                         _stateAnimations.RunTimeScale);
        }

        public void SetStunAnimation()
        {
            SetAnimation(_stateAnimations.Stun, false);
        }

        public void SetStandUpAnimation()
        {
            SetAnimation(_stateAnimations.StandUp, false, 0.4f);
        }

        public bool IsAnimationRun()
        {
            return SkeletonAnimation.AnimationName == _stateAnimations.Run;
        }

        private void OnAnimationCompleted(TrackEntry trackEntry)
        {
            var handler = AnimationCompleted;
            if (handler != null)
            {
                handler(_animationsType[trackEntry.Animation.name]);
            }
        }

        private void SetAnimation(string animationName, bool loop, float timeScale = 1)
        {
            if (SkeletonAnimation.AnimationName == animationName) return;
            
            Contract.Ensure(!string.IsNullOrEmpty(animationName), "animation is not set");

            SkeletonAnimation.loop = loop;
            SkeletonAnimation.AnimationName = animationName;
            SkeletonAnimation.AnimationState.TimeScale = timeScale;
        }

        private IDictionary<string, StateAnimationType> GetAnimationsTypes(IStateAnimations stateAnimations)
        {
            var result = new Dictionary<string, StateAnimationType>();

            if (!string.IsNullOrEmpty(stateAnimations.Idle))
            {
                result[stateAnimations.Idle] = StateAnimationType.Idle;
            }

            if (!string.IsNullOrEmpty(stateAnimations.Run))
            {
                result[stateAnimations.Run] = StateAnimationType.Run;
            }

            if (!string.IsNullOrEmpty(stateAnimations.Fall))
            {
                result[stateAnimations.Fall] = StateAnimationType.Dead;
            }

            if (!string.IsNullOrEmpty(stateAnimations.StandUp))
            {
                result[stateAnimations.StandUp] = StateAnimationType.StandUp;
            }

            if (!string.IsNullOrEmpty(stateAnimations.Stun))
            {
                result[stateAnimations.Stun] = StateAnimationType.Stun;
            }

            return result;
        }

        public void Dispose()
        {
            SkeletonAnimation.AnimationState.Complete -= OnAnimationCompleted;
        }
    }
}
