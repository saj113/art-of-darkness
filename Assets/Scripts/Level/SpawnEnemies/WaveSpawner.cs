using System;
using System.Linq;
using Level.SpawnEnemies.Models;
using Stats;
using UnitControllers;
using UnityEngine;
using Utilities;

namespace Level.SpawnEnemies
{
    public class WaveSpawner : IWaveSpawner
    {
        private const int FinishSubWaveAfterUnitsDieInPercent = 70;
        
        private readonly IUnitGameObjectController _playerGameObjectController;
        private readonly IWave _wave;

        private int _waveUnitsAlive = 0;
        private int _subWaveIndex = 0;
        private bool _isStarted;
        private int _unitsDieToSubWaveFinish = 0;

        public WaveSpawner(
            IUnitGameObjectController playerGameObjectController,
            IWave wave)
        {
            _playerGameObjectController = playerGameObjectController;
            _wave = wave;
        }
        
        public event Action<IWaveSpawner> SpawnWaveFinished;
        public void Start()
        {
            if (_isStarted)
            {
                throw new InvalidOperationException("Wave Spawner has already started.");
            }
            
            _isStarted = true;
            
            SpawnUnits();
        }

        private void SpawnUnits()
        {
            if (_wave.SubWaves.Length >= _subWaveIndex) return;
            
            var subWave = _wave.SubWaves[_subWaveIndex];
            _subWaveIndex++;
            
            _waveUnitsAlive += subWave.Units.Length;
            _unitsDieToSubWaveFinish += ValueUtility.CalculatePercent(
                subWave.Units.Length,
                FinishSubWaveAfterUnitsDieInPercent);

            float j = 0;
            foreach (var unit in subWave.Units)
            {
                var xCenter = GetXCenter(_wave.DistanceBetweenPlayer);
                unit.GameObjectController.Position = new Vector3(
                    xCenter + j, 
                    unit.GameObjectController.Position.y);
                unit.GameObjectController.SetActive(true);
                unit.Characteristics.Died += UnitOnDied;

                if (j >= 0)
                    j += GetDistanceToNextUnit(_wave.RandomDistanceBetweenUnits);

                j *= -1;
            }
        }
        
        private void UnitOnDied(ICharacteristics characteristics)
        {
            characteristics.Died -= UnitOnDied;
            _waveUnitsAlive--;
            _unitsDieToSubWaveFinish--;

            if (_waveUnitsAlive == 0)
            {
                OnSpawnWaveFinished();
                return;
            }

            if (_unitsDieToSubWaveFinish == 0)
            {
                SpawnUnits();
            }
        }

        private float GetXCenter(float distanceBetweenPlayer)
        {
            var randomValue = ValueUtility.GetRandom(0, 10);
            var direction = randomValue == 0 || randomValue % 2 == 0 ? 1 : -1;
            return _playerGameObjectController.Position.x + distanceBetweenPlayer * direction;
        }

        private float GetDistanceToNextUnit(float randomDistanceBetweenUnits)
        {
            return ValueUtility.GetRandom(
                randomDistanceBetweenUnits / 2,
                randomDistanceBetweenUnits);
        }
        
        protected virtual void OnSpawnWaveFinished()
        {
            var handler = SpawnWaveFinished;
            if (handler != null)
            {
                handler(this);
            }
        }
    }
}