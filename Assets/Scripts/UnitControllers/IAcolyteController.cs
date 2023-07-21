using System;
using Stats;
using UnitControllers.AcolytesBehavior;

namespace UnitControllers
{
    public interface IAcolyteController : IDisposable
    {
        void AddAcolyte(IUnitStats unitStats, AcolyteType acolyteType);
    }
}