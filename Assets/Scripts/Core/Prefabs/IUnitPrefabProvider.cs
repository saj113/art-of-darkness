using Stats.Data;

namespace Core.Prefabs
{
    public interface IUnitPrefabProvider
    {
        UnitStatsData GetLevel1Dog();
        UnitStatsData GetLevel1Ghost();
        UnitStatsData GetLevel1SimpleMage();
        UnitStatsData GetLevel1SimpleSoldier();
    }
}