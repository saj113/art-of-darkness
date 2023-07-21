using System;

namespace Skills.Cooldown
{
    public interface ISkillCooldown : IDisposable
    {
        SkillCooldownType Type { get; }
        bool IsReady();
        void GiveCost();
    }
}