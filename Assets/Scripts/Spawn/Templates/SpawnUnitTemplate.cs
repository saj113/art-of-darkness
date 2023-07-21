using Stats.Data;

namespace Spawn.Templates
{
    public class SpawnUnitTemplate : ISpawnUnitTemplate
    {
        public SpawnUnitTemplate()
        {
            DistanceBetweenPlayer = 30;
            RandomDistanceBetweenUnits = 3;
        }

        public int UnitCount { get; set; }
        public int DistanceBetweenPlayer { get; set; }
        public float RandomDistanceBetweenUnits { get; set; }
        public UnitStatsData UnitPrefab { get; set; }
    }
}
