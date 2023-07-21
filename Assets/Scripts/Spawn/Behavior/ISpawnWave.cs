using System;

namespace Spawn.Behavior
{
    public interface ISpawnWave
    {
        event Action<ISpawnWave> SpawnWaveFinished;
        void Spawn();
    }
}
