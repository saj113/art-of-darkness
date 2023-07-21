using Core;
using Stats;

namespace Level.SpawnEnemies.Models
{
    public class SubWave : ISubWave
    {
        public SubWave(IUnitStats[] units)
        {
            Units = units.ThrowIfNull(nameof(units));
        }

        public IUnitStats[] Units { get; }
    }
}