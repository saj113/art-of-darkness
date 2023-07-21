using System;
using Core.Animation;
using UnitControllers;

namespace Stats
{
    public interface IStats : IDisposable
    {
        ICharacteristics Characteristics { get; }
        IUnitGameObjectController GameObjectController { get; }
        IStateController StateController { get; }
        IAcolyteController AcolyteController { get; }
        IStateAnimationController StateAnimationController { get; }
        IBuffController BuffController { get; }
        IAbsorbingBarrierController AbsorbingBarrierController { get; }
        IAgrController AgrController { get; }
    }
}
