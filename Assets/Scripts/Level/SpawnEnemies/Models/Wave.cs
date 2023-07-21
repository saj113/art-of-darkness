using Core;

namespace Level.SpawnEnemies.Models
{
    public class Wave : IWave
    {
        public Wave(ISubWave[] subWaves, int distanceBetweenPlayer, float randomDistanceBetweenUnits)
        {
            SubWaves = subWaves.ThrowIfNull(nameof(subWaves));
            DistanceBetweenPlayer = distanceBetweenPlayer;
            RandomDistanceBetweenUnits = randomDistanceBetweenUnits;
        }

        public int DistanceBetweenPlayer { get; }
        public float RandomDistanceBetweenUnits { get; }
        public ISubWave[] SubWaves { get; }
        
    }
}