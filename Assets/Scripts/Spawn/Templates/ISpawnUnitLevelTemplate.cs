namespace Spawn.Templates
{
    public interface ISpawnLevelTemplate
    {
        int PermissibleDeviationUnitsCount { get; set; }
        ISpawnUnitTemplate[] SpawnUnitTemplates { get; set; }
    }
}