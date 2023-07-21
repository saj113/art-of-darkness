namespace Level.SpawnEnemies.Models
{
    public interface ISpawnLevel
    {
        int LevelWeigh { get; }
        IWaveModel[] WaveModels { get; }
        IUnitType[] UnitTypes { get; }
    }
}