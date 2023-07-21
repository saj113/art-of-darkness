using System.Collections.Generic;
using System.Linq;
using Core.UnityFramework;
using Spawn.Behavior;
using Spawn.Templates;
using Stats;

namespace Spawn
{
    public class SpawnDataConverter : ISpawnDataConverter
    {
        private readonly IGameObjectInstantiater _unityObjectContoller;
        public SpawnDataConverter(IGameObjectInstantiater unityObjectContoller1)
        {
            _unityObjectContoller = unityObjectContoller1;
        }

        public ISpawnWaveLevelParameters[] ConvertToLevelParameterses(
            IEnumerable<ISpawnLevelTemplate> spawnUnitLevels)
        {
            return CreateSpawnWaveLevelParameters(spawnUnitLevels).ToArray();
        }

        private IEnumerable<ISpawnWaveLevelParameters> CreateSpawnWaveLevelParameters(
            IEnumerable<ISpawnLevelTemplate> spawnUnitLevels)
        {
            foreach (var spawnUnitLevel in spawnUnitLevels)
            {
                foreach (var spawnUnitTemplate in spawnUnitLevel.SpawnUnitTemplates)
                {
                    var unitStats = new IUnitStats[spawnUnitTemplate.UnitCount];
                    for (var i = 0; i < spawnUnitTemplate.UnitCount; i++)
                    {
                        unitStats[i] = _unityObjectContoller.Instantiate(
                            spawnUnitTemplate.UnitPrefab, false).Stats;
                    }

                    yield return new SpawnWaveLevelParameters(
                        spawnUnitTemplate.DistanceBetweenPlayer, 
                        spawnUnitTemplate.RandomDistanceBetweenUnits, 
                        unitStats);
                }
            }
        }
    }
}
