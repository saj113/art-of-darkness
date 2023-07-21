using System;
using Core.UnityFramework;
using UnitControllers;

namespace Spawn.Behavior
{
    internal class DelayedSpawnWave : SpawnWave, IDisposable
    {
        private readonly IUnityUpdateEvents _unityUpdateEvents;
        private readonly int _delayedStart;
        private float _timeElapsed;

        public DelayedSpawnWave(
            ISpawnWaveLevelParameters[] spawnWaveLevels, 
            int unitDieToFinish,
            int delayedStart,
            IUnityUpdateEvents unityUpdateEvents,
            IUnitGameObjectController playerGameObjectController) 
            : base(spawnWaveLevels, unitDieToFinish, playerGameObjectController)
        {
            _unityUpdateEvents = unityUpdateEvents;
            _delayedStart = delayedStart;
        }

        public override void Spawn()
        {
            _unityUpdateEvents.FixedUpdateFired += OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            _timeElapsed += deltaTime;
            if (_timeElapsed < _delayedStart) return;

            base.Spawn();
            _unityUpdateEvents.FixedUpdateFired -= OnUpdate;
        }

        public void Dispose()
        {
            _unityUpdateEvents.FixedUpdateFired -= OnUpdate;
        }
    }
}
