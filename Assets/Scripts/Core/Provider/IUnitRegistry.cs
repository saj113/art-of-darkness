using System.Collections.Generic;
using Stats;

namespace Core.Provider
{
    public interface IUnitRegistry
    {
        void Add(IStats unit);
        void Remove(IStats unit);
        void UpdateByTag(IStats unit, Tag oldTag);
    }
}
