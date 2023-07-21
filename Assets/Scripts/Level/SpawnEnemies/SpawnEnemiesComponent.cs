using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Prefabs;
using Core.Trigger;
using Core.UnityFramework;
using Level.SpawnEnemies.Models;
using Store;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Level.SpawnEnemies
{
    public class SpawnEnemiesComponent : MonoBehaviour
    {
        [SerializeField] private SpawnTrigger _spawnTrigger;
        private IDisposable _spawnController;

        void Start()
        {
            _spawnTrigger.ThrowIfNull(nameof(_spawnTrigger));
            
            var levelService = InstanceContainer.Instance.Resolve<ILevelService>();
            var levelPreferences = InstanceContainer.Instance.Resolve<ILevelPreferences>();
            var player = FinderUtility.GetPlayerStats();
            var spawnLevelContainer = new SpawnLevelContainer(new UnitPrefabProvider());
            var waveProvider = new WaveTranslate(new GameObjectInstantiater());
            var waves = waveProvider.Translate(spawnLevelContainer.GetByPlayerLevel(Repository.Instance.Level)).ToArray();
            var spawnTriggerPositions = GetSpawnTriggerPositions(waves.Count(), levelPreferences).ToArray();
            var spawnTriggerController = new SpawnTriggerController(_spawnTrigger, spawnTriggerPositions);
            
            _spawnController = new SpawnController(
                waves, spawnTriggerController, levelService, player.GameObjectController);
        }

        private IEnumerable<float> GetSpawnTriggerPositions(int wavesCount, ILevelPreferences levelPreferences)
        {
            var position = levelPreferences.LevelFightZoneRadius;
            for (var i = 0; i < wavesCount; i++)
            {
                yield return position;
                position += position * 2;
            }
        }
        
        private void OnDestroy()
        {
            _spawnController?.Dispose();
        }
    }
}