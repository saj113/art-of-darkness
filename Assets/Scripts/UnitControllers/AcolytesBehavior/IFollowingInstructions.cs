using System;

namespace UnitControllers.AcolytesBehavior
{
    public interface IFollowingInstructions
    {
        bool IsFolowerValid();
        void GetFollowBounds(Guid unitId, out float from, out float to);
    }
}
