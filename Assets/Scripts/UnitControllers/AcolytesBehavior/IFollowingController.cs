using System;

namespace UnitControllers.AcolytesBehavior
{
    public interface IFollowingController :IDisposable
    {
        bool IsFolowerValid();
        IFollowingInstructions FollowingInstructions { get; set; }
    }
}