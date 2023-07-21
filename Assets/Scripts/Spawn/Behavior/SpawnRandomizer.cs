using System.Collections.Generic;
using System.Linq;
using Core;
using Spawn.Templates;
using Utilities;

namespace Spawn.Behavior
{
    public class SpawnRandomizer : ISpawnRandomizer
    {
        public void RecalculateUnitsCount(IEnumerable<ISpawnLevelTemplate> spawnUnitLevels)
        {
            foreach (var spawnUnitLevel in spawnUnitLevels)
            {
                var unitsCountDictionary = spawnUnitLevel.SpawnUnitTemplates.ToDictionary(p => p, p => p.UnitCount);
                RecalculateUnitsCount(spawnUnitLevel, unitsCountDictionary, true);
            }
        }

        private void RecalculateUnitsCount(
            ISpawnLevelTemplate spawnLevelTemplate,
            IDictionary<ISpawnUnitTemplate, int> unitsCountDictionary,
            bool increace)
        {
            if (unitsCountDictionary.Count < 2)
            {
                return;
            }

            var randomUnitTemplate = unitsCountDictionary.Keys.GetRandomElement();
            unitsCountDictionary.Remove(randomUnitTemplate);

            var increaseAmount = increace ? 1 : -1;
            var randomPermissibleDeviation = ValueUtility.GetRandom(
                0, spawnLevelTemplate.PermissibleDeviationUnitsCount * increaseAmount);
            randomUnitTemplate.UnitCount += randomPermissibleDeviation;

            foreach (var spawnUnitTemplate in spawnLevelTemplate.SpawnUnitTemplates)
            {
                if (randomPermissibleDeviation == 0)
                {
                    break;
                }

                if (unitsCountDictionary.ContainsKey(spawnUnitTemplate))
                {
                    unitsCountDictionary[spawnUnitTemplate] -= increaseAmount;
                    randomPermissibleDeviation -= increaseAmount;
                }
            }

            RecalculateUnitsCount(spawnLevelTemplate, unitsCountDictionary, !increace);
        }
    }
}
