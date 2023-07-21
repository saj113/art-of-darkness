using System;
using Skills.Parameters;

namespace Core.Animation
{
    public interface ISkillAnimationController
    {
        event Action AnimationEvent;
        event Action AnimationComplete;
        void StartSkillAnimation(IAnimationParameters animationParameters);
        void StopSkillAnimation();
    }
}
