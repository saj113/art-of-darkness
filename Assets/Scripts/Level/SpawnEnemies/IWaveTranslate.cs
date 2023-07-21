using System.Collections.Generic;
using Level.SpawnEnemies.Models;

namespace Level.SpawnEnemies
{
    public interface IWaveTranslate
    {
        IEnumerable<IWave> Translate(ISpawnLevel spawnLevel);
    }
}