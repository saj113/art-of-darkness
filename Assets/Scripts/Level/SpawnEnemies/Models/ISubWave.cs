using Stats;

namespace Level.SpawnEnemies.Models
{
    public interface ISubWave
    {
        IUnitStats[] Units { get; }
    }
}