namespace Spawn.Templates
{
    public interface ISpawnWaveTemplate
    {
        int DelayedStart { get; set; }
        int FinishWaveAfteUnitsDieInPercent { get; set; }
        ISpawnLevelTemplate[] SpawnLevelsTemplates { get; set; }
    }
}