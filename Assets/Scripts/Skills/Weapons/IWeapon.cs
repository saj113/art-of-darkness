using System;
using Skills.Parameters;

namespace Skills.Weapons
{
    public interface IWeapon : IDisposable
    {
        event Action SkillAnimationStarted;
        event Action SkillAnimationFinished;
        event Action SkillAnimationInterrupted;
    }
}
