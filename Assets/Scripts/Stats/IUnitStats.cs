using UnitControllers;
using UnitControllers.AcolytesBehavior;

namespace Stats
{
    public interface IUnitStats : IStats
    {
        IFollowingController FollowingController { get; }
        IUnitStateController UnitController { get; }
        void ChangeAgr(ICharacteristics target, int amount);
        event UnitAgrChangedEvent AgrChanged;
    }
}
