using Core;
using UnityEngine;

namespace Skills.Parameters
{
    public class AnimationParameters : IAnimationParameters
    {
        private int _nextAnimationIndex = 0;

        public AnimationParameters(
            float castTime, 
            string animationName,
            AudioClip animationStartAudioClip, 
            AudioClip animationEventAudioClip,
            bool isLoop = false)
            : this(castTime, new [] { animationName }, animationStartAudioClip, animationEventAudioClip, isLoop)
        {
        }

        public AnimationParameters(
            float castTime, 
            string[] animationNames, 
            AudioClip animationStartAudioClip, 
            AudioClip animationEventAudioClip, 
            bool isLoop)
        {
            CastTime = castTime;
            IsLoop = isLoop;
            AnimationNames = animationNames;
            AnimationStartAudioClip = animationStartAudioClip;
            AnimationEventAudioClip = animationEventAudioClip;

            Contract.Ensure(AnimationNames.Length > 0, "AnimationNames has invalid value");
            Contract.Ensure(CastTime > 0.1f, "CastTime has invalid value");
        }


        public float CastTime { get; private set; }
        public bool IsLoop { get; private set; }
        public AudioClip AnimationStartAudioClip { get; private set; }
        public AudioClip AnimationEventAudioClip { get; private set; }

        public string[] AnimationNames { get; private set; }
    }
}