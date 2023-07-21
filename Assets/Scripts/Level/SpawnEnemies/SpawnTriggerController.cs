using System;
using System.Collections.Generic;
using Core;
using Core.Trigger;
using Level.SpawnEnemies.Models;

namespace Level.SpawnEnemies
{
    public class SpawnTriggerController : ISpawnTriggerController
    {
        private readonly ISpawnTrigger _spawnTrigger;
        private readonly Queue<float> _positions;

        public SpawnTriggerController(ISpawnTrigger spawnTrigger, float[] positions)
        {
            _spawnTrigger = spawnTrigger.ThrowIfNull(nameof(spawnTrigger));
            _positions = new Queue<float>(positions.ThrowIfNull(nameof(positions)));
            _spawnTrigger.TriggerEntered += TriggerOnTriggerEntered;
        }

        private void TriggerOnTriggerEntered(ISpawnTrigger trigger)
        {
            trigger.Disable();
            OnPlayerTriggered();
        }

        public event Action PlayerTriggered;
        public void SetNewPosition()
        {
            if (_positions.Count == 0) throw new InvalidOperationException("Cannot set new position for spawn trigger");
            _spawnTrigger.Enable(_positions.Dequeue());
        }

        protected virtual void OnPlayerTriggered()
        {
            PlayerTriggered?.Invoke();
        }

        public void Dispose()
        {
            _spawnTrigger.TriggerEntered -= TriggerOnTriggerEntered;
        }
    }
}