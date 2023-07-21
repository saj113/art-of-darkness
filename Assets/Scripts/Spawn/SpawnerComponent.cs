using System.Collections.Generic;
using Core;
using Core.Prefabs;
using Core.UnityFramework;
using Spawn.Behavior;
using Store;
using UnityEngine;
using Utilities;

namespace Spawn
{
    public class SpawnerComponent : MonoBehaviour
    {
        [SerializeField]
        private int _missionLevel = 1;
        private SpawnController _spawnController;
        private ILevelProgressController _levelProgressController;
        void Start()
        {
            _levelProgressController = new LevelProgressController(Repository.Instance);
            var spawnWaveQueue = GetSpawnWaveQueue();
            _spawnController = new SpawnController(spawnWaveQueue);
            _spawnController.SpawnFinished += OnSpawnFinished;
            _spawnController.StartSpawn();
        }

        void OnDestroy()
        {
            _spawnController.SpawnFinished -= OnSpawnFinished;
        }

        private void OnSpawnFinished()
        {
            _levelProgressController.NotifyMissionCompleted();
        }

        private Queue<ISpawnWave> GetSpawnWaveQueue()
        {
            var contoller = FinderUtility.GetPlayerStats().GameObjectController;
            var spawnRandomizer = new SpawnRandomizer();
            var updateEvents = InstanceContainer.Instance.Resolve<IUnityUpdateEvents>();
            var spawnDataConverter = new SpawnDataConverter(new GameObjectInstantiater());
            var provider = new SpawnWavesProvider(
                spawnRandomizer,
                spawnDataConverter,
                contoller,
                updateEvents,
                new UnitPrefabProvider());

            return provider.GetSpawnWaveQueue(_missionLevel);
        }
    }
}
