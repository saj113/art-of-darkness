namespace Spawn.Templates
{
    public class SpawnWaveTemplate : ISpawnWaveTemplate
    {
        public int DelayedStart { get; set; }
        public int FinishWaveAfteUnitsDieInPercent { get; set; }
        public ISpawnLevelTemplate[] SpawnLevelsTemplates { get; set; }
    }
}
