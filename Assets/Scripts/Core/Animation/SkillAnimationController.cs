using System;
using Core.Controllers;
using Skills.Parameters;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Core.Animation
{
    internal sealed class SkillAnimationController : AnimationController, ISkillAnimationController, IDisposable
    {
        private string _currentSkillAnimationName;
        private IAnimationParameters _currentAnimationParameters;
        private readonly IAudioPlayer _audioPlayer;
        private string _lastAnimationName;
        public SkillAnimationController(
            SkeletonAnimation skeletonAnimation,
            IAudioPlayer audioPlayer,
            ILogger logger) : base(skeletonAnimation, logger)
        {
            _audioPlayer = audioPlayer;
        }

        public event Action AnimationEvent;

        public event Action AnimationComplete;

        public void StartSkillAnimation(IAnimationParameters animationParameters)
        {
            _lastAnimationName = GetNextAnimationName(animationParameters.AnimationNames);
            _currentSkillAnimationName = _lastAnimationName;
            _currentAnimationParameters = animationParameters;

            SkeletonAnimation.AnimationState.ClearTracks();
            SkeletonAnimation.AnimationState.Start += AnimationStateOnStart;
            SkeletonAnimation.loop = animationParameters.IsLoop;
            SkeletonAnimation.AnimationName = _currentSkillAnimationName;
            SkeletonAnimation.AnimationState.TimeScale =
                DefaultAnimationDuration /  animationParameters.CastTime;
        }

        public void StopSkillAnimation()
        {
            SkeletonAnimation.loop = false;
        }

        private void AnimationStateOnComplete(TrackEntry trackEntry)
        {
            if (SkeletonAnimation.loop)
            {
                return;
            }

            Unregister();

            _audioPlayer.Stop();

            if (SkeletonAnimation.AnimationName == _currentSkillAnimationName)
            {
                SkeletonAnimation.AnimationName = null;
            }
            _currentSkillAnimationName = null;
            _currentAnimationParameters = null;
            OnAnimationComplete();
        }

        private void AnimationStateOnStart(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name != _currentSkillAnimationName) return;
            SkeletonAnimation.AnimationState.Complete += AnimationStateOnComplete;
            SkeletonAnimation.AnimationState.Event    += AnimationControllerOnAnimationEvent;
            PlayAnimationSoundIfExist(_currentAnimationParameters.AnimationStartAudioClip);
        }

        private void Unregister()
        {
            SkeletonAnimation.AnimationState.Start -= AnimationStateOnStart;
            SkeletonAnimation.AnimationState.Complete -= AnimationStateOnComplete;
            SkeletonAnimation.AnimationState.Event -= AnimationControllerOnAnimationEvent;
        }

        private void AnimationControllerOnAnimationEvent(TrackEntry trackEntry, Spine.Event e)
        {
            PlayAnimationSoundIfExist(_currentAnimationParameters.AnimationEventAudioClip);
            OnAnimationEvent();
        }

        private void PlayAnimationSoundIfExist(AudioClip clip)
        {
            if (clip != null)
            {
                _audioPlayer.PlayOneShot(clip);
            }
        }

        private void OnAnimationEvent()
        {
            var handler = AnimationEvent;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnAnimationComplete()
        {
            var handler = AnimationComplete;
            if (handler != null)
            {
                handler();
            }
        }
        
        private string GetNextAnimationName(string[] animations)
        {
            if (animations.Length == 1 || string.IsNullOrEmpty(_lastAnimationName))
            {
                return animations[0];
            }

            var index = Array.IndexOf(animations, _lastAnimationName);

            if (index == -1)
            {
                return animations[0];
            }

            return index == animations.Length - 1 ? animations[0] : animations[index + 1];
        }

        public void Dispose() { Unregister(); }
    }
}
