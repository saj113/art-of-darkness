using Stats.Data;

namespace Level.SpawnEnemies.Models
{
    public interface IUnitType
    {
        int Weigh { get; }
        UnitStatsData UnitPrefab { get; }
    }
}