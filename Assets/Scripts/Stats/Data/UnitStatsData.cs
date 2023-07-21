using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Animation;
using Core.Controllers;
using Core.Prefabs;
using Core.Provider;
using Core.UnityFramework;
using Level;
using Skills;
using Skills.Parameters;
using Skills.Weapons;
using Spine.Unity;
using UnitControllers;
using UnitControllers.AbsorbingBarrierBehavior;
using UnitControllers.AcolytesBehavior;
using UnitControllers.AgrBehavior;
using UnitControllers.BuffsBehavior;
using UnitControllers.DetectionTargets;
using UnitControllers.MovementsBehavior;
using UnitControllers.StatesBehavior;
using UnitControllers.UnitGameObjectBehavior;
using UnityEngine;
using ILogger = Core.ILogger;

namespace Stats.Data
{
    public class UnitStatsData : StatsData<IUnitStats>
    {
        [SerializeField] private int _level;
        [SerializeField] private StateAnimationTemplate _stateAnimationTemplate;
        [SerializeField] private TargetUnitRelation _targetRelation = TargetUnitRelation.Enemy;
        [SerializeField] private DetectionTargetBehavior _detectionTargetBehavior;
        [SerializeField] private List<UnitSkillInfo> _skills;
        [SerializeField] private MobHealthbarData _mobHealthbarData;

        public int Level => _level;

        protected override IUnitStats SetupStats()
        {
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.ScriptOnly);

            var unitId = Guid.NewGuid();
            var logger = GetLogger(unitId);
            var updateEvents = InstanceContainer.Instance.Resolve<IUnityUpdateEvents>();
            var targetUnitProvider = InstanceContainer.Instance.Resolve<ITargetUnitProvider>();
            var levelPreferences = InstanceContainer.Instance.Resolve<ILevelPreferences>();
            var agrController = new AgrController();
            var absorbingBarrierController = new AbsorbingBarrierController();
            var buffController = new BuffController(updateEvents);
            var skillAnimationController = new SkillAnimationController(
                    SkeletonAnimation,
                    new AudioPlayer(GetComponent<AudioSource>()),
                    logger);
            var stateAnimationController = new StateAnimationController(
                    SkeletonAnimation,
                    StateAnimationsProvider.GetByTemplate(_stateAnimationTemplate),
                    logger);
            var characteristics = GetCharacteristics(unitId,
                                                     _level,
                                                     absorbingBarrierController,
                                                     updateEvents);
            var unitGameObjectController = new UnitGameObjectController(characteristics, gameObject, levelPreferences);
            var follower = new Follower(characteristics, unitGameObjectController);
            var acolyteController = new AcolyteController(follower);
            var followingController = new FollowingController(characteristics);
            var healthBarTransform = CreateHealthBar();

            var stateController = GetStateController(unitId,
                                                     skillAnimationController,
                                                     stateAnimationController,
                                                     unitGameObjectController,
                                                     characteristics,
                                                     acolyteController,
                                                     followingController,
                                                     agrController,
                                                     logger,
                                                     updateEvents,
                                                     targetUnitProvider);

            return new UnitStats(characteristics,
                                 stateController,
                                 absorbingBarrierController,
                                 buffController,
                                 acolyteController,
                                 stateAnimationController,
                                 unitGameObjectController,
                                 followingController,
                                 healthBarTransform,
                                 agrController);
        }

        private ITransform CreateHealthBar()
        {
            var healthBarInstance = Instantiate(_mobHealthbarData.SpriteRendererPrefab, transform, true);
            healthBarInstance.transform.position = new Vector2(transform.position.x, _mobHealthbarData.YOffset);
            return new UnityTransform(healthBarInstance.transform);
        }

        private IUnitStateController GetStateController(Guid unitId,
                                                        ISkillAnimationController skillAnimationController,
                                                        IStateAnimationController stateAnimationController,
                                                        IUnitGameObjectController unitGameObjectController,
                                                        ICharacteristics characteristics,
                                                        IAcolyteController acolyteController,
                                                        IFollowingController followingController,
                                                        IAgrController agrController,
                                                        Core.ILogger logger,
                                                        IUnityUpdateEvents updateEvents,
                                                        ITargetUnitProvider targetUnitProvider)
        {
            var skillCaster = new SkillCaster(characteristics,
                                              unitGameObjectController,
                                              acolyteController,
                                              agrController,
                                              _targetRelation);
            var unitWeapon = GetUnitWeapon(skillCaster,
                                           skillAnimationController,
                                           updateEvents,
                                           targetUnitProvider);
            var priorityTargetProvider = GetPriorityTargetProvider(unitId, skillCaster, targetUnitProvider, logger);

            return new UnitStateController(stateAnimationController,
                                           updateEvents,
                                           unitWeapon,
                                           unitGameObjectController,
                                           new MovementController(unitGameObjectController,
                                                                  characteristics,
                                                                  stateAnimationController),
                                           priorityTargetProvider,
                                           characteristics,
                                           followingController,
                                           logger);
        }

        private IPriorityTargetProvider GetPriorityTargetProvider(Guid unitId,
                                                                  ISkillCaster skillCaster,
                                                                  ITargetUnitProvider targetUnitProvider,
                                                                  ILogger logger)
        {
            var algorithm = GetDetectTargetAlgorithm(unitId);
            return new PriorityTargetProvider(skillCaster, targetUnitProvider, algorithm, logger);
        }

        private IDetectTargetAlgorithm GetDetectTargetAlgorithm(Guid unitId)
        {
            switch (_detectionTargetBehavior)
            {
                case DetectionTargetBehavior.PlayerTarget: return new PlayerTargetAlgorithm();

                case DetectionTargetBehavior.AggressiveTarget:
                    return new AggressiveTargetAlgorithmcs(unitId, new NearestTargetAlgorithm());

                default: return new NearestTargetAlgorithm();
            }
        }

        private IUnitWeapon GetUnitWeapon(ISkillCaster skillCaster,
                                          ISkillAnimationController skillAnimationController,
                                          IUnityUpdateEvents updateEvents,
                                          ITargetUnitProvider targetUnitProvider)
        {
            var colliderActivator = new SkillColliderActivator(GameObjectInstantiater, updateEvents);
            return new UnitWeapon(new SkillBehaviorProvider(skillCaster,
                                                            GameObjectInstantiater,
                                                            colliderActivator,
                                                            targetUnitProvider),
                                  skillAnimationController,
                                  GetSkillParameters(skillCaster, updateEvents));
        }

        private ISkillParameters[] GetSkillParameters(ISkillCaster skillCaster, IUnityUpdateEvents updateEvents)
        {
            var unitSkillPrefabProvider = InstanceContainer.Instance.Resolve<IUnitSkillPrefabProvider>();
            var skillProvider = new UnitSkillsProvider(skillCaster, unitSkillPrefabProvider, updateEvents);
            return _skills.Select(info => skillProvider.GetSkill(info)).ToArray();
        }
    }
}
