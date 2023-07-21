using System;
using System.Collections.Generic;
using Core;
using Core.UnityFramework;
using Level.SpawnEnemies.Models;
using Spawn.Behavior;
using UnitControllers;

namespace Level.SpawnEnemies
{
    public class SpawnController : IDisposable
    {
        private readonly Queue<IWave> _wavesQueue;
        private readonly ISpawnTriggerController _spawnTriggerController;
        private readonly ILevelService _levelService;
        private readonly IUnitGameObjectController _playerGameObjectController;

        public SpawnController(
            IEnumerable<IWave> waves, 
            ISpawnTriggerController spawnTriggerController,
            ILevelService levelService,
            IUnitGameObjectController playerGameObjectController)
        {
            _wavesQueue = new Queue<IWave>(waves.ThrowIfNull(nameof(waves)));
            _spawnTriggerController = spawnTriggerController;
            _levelService = levelService;
            _playerGameObjectController = playerGameObjectController;
            _spawnTriggerController.SetNewPosition();
            _spawnTriggerController.PlayerTriggered += OnPlayerTriggered;
        }

        private void OnSpawnWaveFinished(IWaveSpawner spawner)
        {
            spawner.SpawnWaveFinished -= OnSpawnWaveFinished;
            
            if (_wavesQueue.Count == 0)
            {
                _levelService.FinishLevel(_playerGameObjectController.Position.x);
            }
            else
            {
                _levelService.DisableFightZone();
                _spawnTriggerController.SetNewPosition();
            }
        }

        private void OnPlayerTriggered()
        {
            if (_wavesQueue.Count < 1)
            {
                throw new InvalidOperationException("Waves queue is empty");
            }

            _levelService.EnableFightZone(_playerGameObjectController.Position.x);
            var wave = _wavesQueue.Dequeue();
            var spawner = new WaveSpawner(_playerGameObjectController, wave);
            spawner.SpawnWaveFinished += OnSpawnWaveFinished;
            spawner.Start();
        }

        public void Dispose()
        {
            _spawnTriggerController.PlayerTriggered -= OnPlayerTriggered;
        }
    }
}