using System.Collections.Generic;
using Spawn.Behavior;

namespace Spawn
{
    public interface ISpawnWavesProvider
    {
        Queue<ISpawnWave> GetSpawnWaveQueue(int level);
    }
}
