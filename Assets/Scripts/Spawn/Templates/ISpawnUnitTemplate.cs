using Stats.Data;

namespace Spawn.Templates
{
    public interface ISpawnUnitTemplate
    {
        int UnitCount { get; set; }
        int DistanceBetweenPlayer { get; set; }
        float RandomDistanceBetweenUnits { get; set; }
        UnitStatsData UnitPrefab { get; set; }
    }
}