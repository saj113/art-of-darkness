using Stats;

namespace Level.SpawnEnemies.Models
{
    public interface IWave
    {
        int DistanceBetweenPlayer { get; }
        float RandomDistanceBetweenUnits { get; }
        ISubWave[] SubWaves { get; }
    }
}