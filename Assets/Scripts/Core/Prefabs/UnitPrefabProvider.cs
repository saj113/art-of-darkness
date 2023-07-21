using Stats.Data;

namespace Core.Prefabs
{
    public class UnitPrefabProvider : PrefabProvider, IUnitPrefabProvider
    {
        public UnitStatsData GetLevel1SimpleSoldier() { return GetPrefab<UnitStatsData>("Units/1/SimpleSoldier"); }
        public UnitStatsData GetLevel1SimpleMage() { return GetPrefab<UnitStatsData>("Units/1/SimpleMage"); }
        public UnitStatsData GetLevel1Ghost() { return GetPrefab<UnitStatsData>("Units/1/Ghost"); }
        public UnitStatsData GetLevel1Dog() { return GetPrefab<UnitStatsData>("Units/1/Dog"); }
    }
}
