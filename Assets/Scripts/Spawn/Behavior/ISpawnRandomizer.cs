using System.Collections.Generic;
using Spawn.Templates;

namespace Spawn.Behavior
{
    public interface ISpawnRandomizer
    {
        void RecalculateUnitsCount(IEnumerable<ISpawnLevelTemplate> spawnUnitLevels);
    }
}