namespace Spawn.Templates
{
    public class SpawnLevelTemplateTemplate : ISpawnLevelTemplate
    {
        public int PermissibleDeviationUnitsCount { get; set; }
        public ISpawnUnitTemplate[] SpawnUnitTemplates { get; set; }
    }
}
