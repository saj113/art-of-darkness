using System.Collections.Generic;
using System.Linq;
using Core.UnityFramework;
using Level.SpawnEnemies.Models;
using Stats;
using Stats.Data;
using Utilities;

namespace Level.SpawnEnemies
{
    public class WaveTranslate : IWaveTranslate
    {
        private const int DistanceBetweenPlayer = 30;
        private const int RandomDistanceBetweenUnits = 3;
        private readonly IGameObjectInstantiater _unityObjectController;
        public WaveTranslate(IGameObjectInstantiater unityObjectController)
        {
            _unityObjectController = unityObjectController;
        }
        
        public IEnumerable<IWave> Translate(ISpawnLevel spawnLevel)
        {
            foreach (var waveModel in spawnLevel.WaveModels)
            {
                var waveWeigh = ValueUtility.CalculatePercent(spawnLevel.LevelWeigh, waveModel.WeighPercent);
                var unitStatsCollection = GetWaveUnitPrefabs(waveWeigh, spawnLevel.UnitTypes)
                    .Select(GetUnitStats)
                    .ToArray();
                var subWaves = GetSubWaves(unitStatsCollection, waveModel.SubWeighPercents).ToArray();
                yield return new Wave(subWaves, DistanceBetweenPlayer, RandomDistanceBetweenUnits);
            }
        }

        private IEnumerable<UnitStatsData> GetWaveUnitPrefabs(int waveWeigh, IUnitType[] unitTypes)
        {
            var calculatedWeigh = 0;
            while (calculatedWeigh < waveWeigh)
            {
                var maxUnitWeigh = unitTypes.Max(p => p.Weigh);
                var randomMax = waveWeigh - calculatedWeigh <= maxUnitWeigh
                    ? waveWeigh - calculatedWeigh
                    : maxUnitWeigh;
                var randomWeigh = ValueUtility.GetRandom(1, randomMax);
                var unitType = unitTypes
                    .Where(p => p.Weigh <= randomWeigh)
                    .OrderByDescending(p => p.Weigh)
                    .First();

                calculatedWeigh += unitType.Weigh;
                yield return unitType.UnitPrefab;
            }
        }

        private IEnumerable<ISubWave> GetSubWaves(IUnitStats[] waveUnits, int[] subWavesPercents)
        {
            var skipUnits = 0;
            for (var i = 0; i < subWavesPercents.Length; i++)
            {
                if (i == subWavesPercents.Length - 1)
                {
                    yield return new SubWave(waveUnits.Skip(skipUnits).ToArray());
                    break;
                }
                
                var percent = subWavesPercents[i];
                var subWaveUnitsCount = ValueUtility.CalculatePercent(waveUnits.Length, percent);
                var subWaveUnits = waveUnits.Skip(skipUnits).Take(subWaveUnitsCount).ToArray();
                skipUnits += subWaveUnitsCount;
                
                yield return new SubWave(subWaveUnits);
            }
        }

        private IUnitStats GetUnitStats(UnitStatsData prefab)
        {
            return _unityObjectController.Instantiate(prefab, false).Stats;
        }
    }
}