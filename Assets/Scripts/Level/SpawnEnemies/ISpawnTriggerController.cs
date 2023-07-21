using System;

namespace Level.SpawnEnemies
{
    public interface ISpawnTriggerController : IDisposable
    {
        event Action PlayerTriggered;
        void SetNewPosition();
    }
}