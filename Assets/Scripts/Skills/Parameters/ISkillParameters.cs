using System;
using Skills.Parameters.BehaviorParameters;

namespace Skills.Parameters
{
    public interface ISkillParameters : IDisposable
    {
        IGeneralParameters General { get; }
        IAnimationParameters Animation { get; }
        IBehaviorParameters BehaviorParameters { get; }
    }
}
