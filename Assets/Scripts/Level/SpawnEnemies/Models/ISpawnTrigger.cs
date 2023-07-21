using System;

namespace Level.SpawnEnemies.Models
{
    public interface ISpawnTrigger
    {
        event Action<ISpawnTrigger> TriggerEntered;
        void Disable();
        void Enable(float position);
    }
}