using System;
using Level.SpawnEnemies.Models;

namespace Level.SpawnEnemies
{
    public interface IWaveSpawner
    {
        event Action<IWaveSpawner> SpawnWaveFinished;
        void Start();
    }
}