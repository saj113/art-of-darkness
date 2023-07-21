using System.Collections.Generic;
using Spawn.Behavior;
using Spawn.Templates;

namespace Spawn
{
    public interface ISpawnDataConverter
    {
        ISpawnWaveLevelParameters[] ConvertToLevelParameterses(
            IEnumerable<ISpawnLevelTemplate> spawnUnitLevels);
    }
}