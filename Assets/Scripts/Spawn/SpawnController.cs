using System;
using System.Collections.Generic;
using Spawn.Behavior;
using Stats;

namespace Spawn
{
    internal class SpawnController
    {
        private readonly Queue<ISpawnWave> _spawnWavesQueue;
        private readonly IStats _player;

        public SpawnController(Queue<ISpawnWave> spawnWavesQueue)
        {
            _spawnWavesQueue = spawnWavesQueue;
        }

        public event Action SpawnFinished;

        public void StartSpawn()
        {
            SpawnNextWave(null);
        }

        private void SpawnNextWave(ISpawnWave previousSpawnWave)
        {
            if (previousSpawnWave != null)
            {
                previousSpawnWave.SpawnWaveFinished -= SpawnNextWave;
            }

            if (_spawnWavesQueue.Count == 0)
            {
                OnSpawnFinished();
                return;
            }

            var nextWave = _spawnWavesQueue.Dequeue();
            nextWave.SpawnWaveFinished += SpawnNextWave;
            nextWave.Spawn();
        } 

        protected void OnSpawnFinished()
        {
            var handler = SpawnFinished;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
