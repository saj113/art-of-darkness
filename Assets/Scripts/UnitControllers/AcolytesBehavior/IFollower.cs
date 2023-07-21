using System;

namespace UnitControllers.AcolytesBehavior
{
    public interface IFollower : IFollowingInstructions
    {
         void AddAcolyte(Guid id);
         void RemoveAcolyte(Guid id);
    }
}