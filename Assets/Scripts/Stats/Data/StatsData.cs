using System;
using Core;
using Core.Provider;
using Core.UnityFramework;
using Spine.Unity;
using UnitControllers;
using UnityEngine;

namespace Stats.Data
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class StatsData<T> : MonoBehaviour where T : IStats
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private Tag _tag;
        [SerializeField] private CharacteristicsTemplate _characteristicsTemplate;

        private readonly IGameObjectInstantiater _gameObjectInstantiater =
                new GameObjectInstantiater();

        private T _stats;

        protected SkeletonAnimation SkeletonAnimation => _skeletonAnimation;

        public T Stats
        {
            get
            {
                Initialize();
                return _stats;
            }
        }

        protected IGameObjectInstantiater GameObjectInstantiater
        {
            get { return _gameObjectInstantiater; }
        }

        protected Core.ILogger GetLogger(Guid unitId)
        {
            return new DebugLogger(
                    string.Format(
                            "{0}_{1}_{2}",
                            _tag,
                            _characteristicsTemplate,
                            unitId.ToString()));
        }

        protected abstract T SetupStats();

        void Start() { Initialize(); }

        private void Initialize()
        {
            if (_stats == null)
            {
                var unitRegistry = InstanceContainer.Instance.Resolve<IUnitRegistry>();
                _stats = SetupStats();
                unitRegistry.Add(_stats);
                _stats.GameObjectController.Destroyed += () => { unitRegistry.Remove(_stats); };
                _stats.Characteristics.TagChanged += oldTag =>
                                                     {
                                                         unitRegistry.UpdateByTag(_stats, oldTag);
                                                     };
            }
        }

        void OnDestroy() { Stats.Dispose(); }

        protected ICharacteristics GetCharacteristics(
            Guid unitId,
            int level,
            IAbsorbingBarrierController absorbingBarrierController,
            IUnityUpdateEvents updateEvents)
        {
            return new CharacteristicsProvider().GetCharacteristics(
                level,
                unitId,
                _characteristicsTemplate,
                _tag,
                absorbingBarrierController,
                updateEvents);
        }
    }
}
