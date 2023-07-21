using System.Collections.Generic;
using System.Linq;
using Core.Prefabs;
using Core.UnityFramework;
using Spawn.Behavior;
using Spawn.Templates;
using UnitControllers;
using Utilities;

namespace Spawn
{
    public class SpawnWavesProvider : ISpawnWavesProvider
    {
        private readonly IDictionary<int, ISpawnWaveTemplate[]> _spawnWavesByMission;

        private readonly ISpawnRandomizer _spawnRandomizer;
        private readonly ISpawnDataConverter _spawnDataConverter;
        private readonly IUnitGameObjectController _characterGameObjectController;
        private readonly IUnityUpdateEvents _updateEvents;
        private readonly IUnitPrefabProvider _unitPrefabProvider;

        public SpawnWavesProvider(
            ISpawnRandomizer spawnRandomizer,
            ISpawnDataConverter spawnDataConverter,
            IUnitGameObjectController characterGameObjectController,
            IUnityUpdateEvents updateEvents,
            IUnitPrefabProvider unitPrefabProvider)
        {
            _spawnDataConverter = spawnDataConverter;
            _spawnRandomizer = spawnRandomizer;
            _characterGameObjectController = characterGameObjectController;
            _updateEvents = updateEvents;
            _unitPrefabProvider = unitPrefabProvider;
            _spawnWavesByMission = InitializeSpawnWaves();
        }

        public Queue<ISpawnWave> GetSpawnWaveQueue(int missionLevel)
        {
            var result = new Queue<ISpawnWave>();
            var wavesTemplates = _spawnWavesByMission[missionLevel];
            foreach (var waveTemplate in wavesTemplates)
            {
                _spawnRandomizer.RecalculateUnitsCount(waveTemplate.SpawnLevelsTemplates);
                result.Enqueue(GetSpawnWave(waveTemplate));
            }

            return result;
        }

        private ISpawnWave GetSpawnWave(
            ISpawnWaveTemplate spawnWaveTemplate)
        {
            var spawnWaveLevelParameters = _spawnDataConverter.ConvertToLevelParameterses(
                spawnWaveTemplate.SpawnLevelsTemplates);
            var unitDieToFinish = ValueUtility.CalculatePercent(
                spawnWaveLevelParameters.Select(p => p.Units.Length).Sum(),
                spawnWaveTemplate.FinishWaveAfteUnitsDieInPercent);

            if (spawnWaveTemplate.DelayedStart > 0)
            {
                return new DelayedSpawnWave(
                    spawnWaveLevelParameters,
                    unitDieToFinish,
                    spawnWaveTemplate.DelayedStart,
                    _updateEvents,
                    _characterGameObjectController);
            }

            return new SpawnWave(spawnWaveLevelParameters, unitDieToFinish, _characterGameObjectController);
        }

        private IDictionary<int, ISpawnWaveTemplate[]> InitializeSpawnWaves()
        {
            var result = new Dictionary<int, ISpawnWaveTemplate[]>();
            _spawnWavesByMission[1] = GetSpawnWaveTemplatesLevel1();

            return result;
        }

        #region spawn waves
        private ISpawnWaveTemplate[] GetSpawnWaveTemplatesLevel1()
        {
            var wave1 = new SpawnWaveTemplate
            {
                FinishWaveAfteUnitsDieInPercent = 80,
                SpawnLevelsTemplates = new[]
                {
                    new SpawnLevelTemplateTemplate
                    {
                        PermissibleDeviationUnitsCount = 2,
                        SpawnUnitTemplates = new[]
                        {
                            new SpawnUnitTemplate
                            {
                                UnitCount = 3,
                                UnitPrefab = _unitPrefabProvider.GetLevel1SimpleMage()
                            },
                            new SpawnUnitTemplate
                            {
                                UnitCount = 3,
                                UnitPrefab = _unitPrefabProvider.GetLevel1SimpleSoldier()
                            }
                        }
                    }
                }
            };

            var wave2 = new SpawnWaveTemplate
            {
                FinishWaveAfteUnitsDieInPercent = 50,
                SpawnLevelsTemplates = new[]
                {
                    new SpawnLevelTemplateTemplate
                    {
                        PermissibleDeviationUnitsCount = 3,
                        SpawnUnitTemplates = new []
                        {
                            new SpawnUnitTemplate
                            {
                                UnitCount = 6,
                                UnitPrefab = _unitPrefabProvider.GetLevel1SimpleSoldier()
                            },
                            new SpawnUnitTemplate
                            {
                                UnitCount = 3,
                                UnitPrefab = _unitPrefabProvider.GetLevel1Dog()
                            }
                        }
                    }
                }
            };

            var wave3 = new SpawnWaveTemplate
            {
                FinishWaveAfteUnitsDieInPercent = 100,
                SpawnLevelsTemplates = new[]
                {
                    new SpawnLevelTemplateTemplate
                    {
                        SpawnUnitTemplates = new []
                        {
                            new SpawnUnitTemplate
                            {
                                UnitCount = 5,
                                UnitPrefab = _unitPrefabProvider.GetLevel1SimpleSoldier()
                            }
                        }
                    },
                }
            };

            return new ISpawnWaveTemplate[] {wave1, wave2, wave3};
        }

        #endregion
    }
}