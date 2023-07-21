using UnityEngine;

namespace Skills.Parameters
{
    public interface IAnimationParameters
    {
        float CastTime { get; }
        bool IsLoop { get; }
        AudioClip AnimationStartAudioClip { get; }
        AudioClip AnimationEventAudioClip { get; }
        string[] AnimationNames { get; }
    }
}
