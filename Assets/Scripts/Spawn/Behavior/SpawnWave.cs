using System;
using Stats;
using UnitControllers;
using UnityEngine;
using Utilities;

namespace Spawn.Behavior
{
    internal class SpawnWave : ISpawnWave
    {
        private readonly ISpawnWaveLevelParameters[] _spawnWaveLevels;
        private readonly int _unitDieToFinish;
        private int _unitsDeadCount;
        private readonly IUnitGameObjectController _playerGameObjectController;
        public SpawnWave(ISpawnWaveLevelParameters[] spawnWaveLevels, int unitDieToFinish, IUnitGameObjectController playerGameObjectController)
        {
            _playerGameObjectController = playerGameObjectController;
            _spawnWaveLevels = spawnWaveLevels;
            _unitDieToFinish = unitDieToFinish;
        }

        public event Action<ISpawnWave> SpawnWaveFinished;

        public virtual void Spawn()
        {
            foreach (var spawnWaveLevelParameters in _spawnWaveLevels)
            {
                Spawn(spawnWaveLevelParameters);
            }
        }

        private void Spawn(ISpawnWaveLevelParameters spawnWaveLevelParameters)
        {
            float j = 0;
            foreach (var unit in spawnWaveLevelParameters.Units)
            {
                var xCenter = GetXCenter(spawnWaveLevelParameters.DistanceBetweenPlayer);
                unit.GameObjectController.Position = new Vector3(
                    xCenter + j, 
                    unit.GameObjectController.Position.y);
                unit.GameObjectController.SetActive(true);
                unit.Characteristics.Died += UnitOnDied;

                if (j >= 0)
                    j += GetDistanceToNextUnit(spawnWaveLevelParameters.RandomDistanceBetweenUnits);

                j *= -1;
            }
        }

        private void UnitOnDied(ICharacteristics characteristics)
        {
            characteristics.Died -= UnitOnDied;
            _unitsDeadCount++;

            if (_unitsDeadCount == _unitDieToFinish)
            {
                OnSpawnWaveFinished();
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
