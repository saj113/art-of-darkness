using System;
using System.Collections.Generic;

namespace UnitControllers.AbsorbingBarrierBehavior
{
    public class AbsorbingBarrierController : IAbsorbingBarrierController
    {
        private readonly HashSet<AbsorbingBarrier> _absorbingBarrierCollection = 
            new HashSet<AbsorbingBarrier>();

        public int Absorb(int damage)
        {
            var damagePower = Math.Abs(damage);
            foreach (var absorbingBarrier in _absorbingBarrierCollection)
            {
                damagePower = absorbingBarrier.Absorb(damagePower);
            }
            return damagePower * -1;
        }

        public void AddBarrier(AbsorbingBarrier absorbingBarrier)
        {
            _absorbingBarrierCollection.Add(absorbingBarrier);
        }

        public void RemoveBarrier(AbsorbingBarrier absorbingBarrier)
        {
            _absorbingBarrierCollection.Remove(absorbingBarrier);
        }
    }
}
