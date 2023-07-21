using UnitControllers.AbsorbingBarrierBehavior;

namespace UnitControllers
{
    public interface IAbsorbingBarrierController
    {
        int Absorb(int damage);
        void AddBarrier(AbsorbingBarrier absorbingBarrier);
        void RemoveBarrier(AbsorbingBarrier absorbingBarrier);
    }
}