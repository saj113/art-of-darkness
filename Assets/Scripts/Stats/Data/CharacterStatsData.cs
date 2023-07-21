using System;
using System.Collections.Generic;
using Core;
using Core.Animation;
using Core.Controllers;
using Core.Prefabs;
using Core.Provider;
using Core.UnityFramework;
using GUIScripts.Messengers;
using Level;
using Skills;
using Skills.Weapons;
using Spine.Unity;
using Store;
using UnitControllers;
using UnitControllers.AbsorbingBarrierBehavior;
using UnitControllers.AcolytesBehavior;
using UnitControllers.AgrBehavior;
using UnitControllers.BuffsBehavior;
using UnitControllers.MovementsBehavior;
using UnitControllers.StatesBehavior;
using UnitControllers.TouchControllers;
using UnitControllers.TouchControllers.ShapesRecogntions;
using UnitControllers.UnitGameObjectBehavior;
using UnityEngine;

namespace Stats.Data
{
    [RequireComponent(typeof(LineRenderer))]
    public class CharacterStatsData : StatsData<ICharacterStats>
    {
        protected override ICharacterStats SetupStats()
        {
            var skeletonAnimation = SkeletonAnimation;
            Contract.Require(skeletonAnimation != null, "skeletonAnimation");

            var unitId = Guid.NewGuid();
            var logger = GetLogger(unitId);
            var targetUnitProvider = InstanceContainer.Instance.Resolve<ITargetUnitProvider>();
            var updateEvents = InstanceContainer.Instance.Resolve<IUnityUpdateEvents>();
            var skillUseFailedMessenger = InstanceContainer.Instance.Resolve<ISkillUseFailedMessenger>();
            var levelPreferences = InstanceContainer.Instance.Resolve<ILevelPreferences>();
            var absorbingBarrierController = new AbsorbingBarrierController();
            var buffController = new BuffController(updateEvents);
            var skillAnimationController = new SkillAnimationController(
                    skeletonAnimation,
                    new AudioPlayer(GetComponent<AudioSource>()),
                    logger);
            var stateAnimationController = new StateAnimationController(
                    skeletonAnimation,
                    StateAnimationsProvider.GetByTemplate(StateAnimationTemplate.Necromancer),
                    logger);
            var characteristics = GetCharacteristics(
                    unitId,
                    Repository.Instance.Level,
                    absorbingBarrierController,
                    updateEvents);
            var unitGameObjectController =
                    new UnitGameObjectController(characteristics, gameObject, levelPreferences);
            var follower = new Follower(characteristics, unitGameObjectController);
            var acolyteController = new AcolyteController(follower);
            var agrController = new AgrController();
            var characterMovementInfo = new CharacterMovementInfo();

            var characterWeapon = GetCharacterWeapon(
                    skillAnimationController,
                    unitGameObjectController,
                    characteristics,
                    acolyteController,
                    agrController,
                    characterMovementInfo,
                    updateEvents,
                    logger,
                    targetUnitProvider);

            var stateController = GetStateController(
                    stateAnimationController,
                    unitGameObjectController,
                    characteristics,
                    characterWeapon,
                    characterMovementInfo,
                    logger,
                    updateEvents,
                    skillUseFailedMessenger);

            return new CharacterStats(
                    characteristics,
                    stateController,
                    absorbingBarrierController,
                    buffController,
                    acolyteController,
                    stateAnimationController,
                    unitGameObjectController,
                    characterWeapon,
                    agrController);
        }

        private ICharacterStateController GetStateController(
                IStateAnimationController stateAnimationController,
                IUnitGameObjectController unitGameObjectController,
                ICharacteristics characteristics,
                ICharacterWeapon characterWeapon,
                ICharacterMovementInfo characterMovementInfo,
                Core.ILogger logger,
                IUnityUpdateEvents unityUpdateEvents,
                ISkillUseFailedMessenger skillUseFailedMessenger)
        {
            return new CharacterStateController(
                    stateAnimationController,
                    unityUpdateEvents,
                    characterWeapon,
                    unitGameObjectController,
                    new MovementController(
                            unitGameObjectController,
                            characteristics,
                            stateAnimationController),
                    new TouchController(
                            new SpecificPointFinder(),
                            GetShapesRecognizers(),
                            unityUpdateEvents,
                            GetLineRenderer()),
                    characterMovementInfo,
                    skillUseFailedMessenger,
                    logger);
        }

        private ICharacterWeapon GetCharacterWeapon(
                ISkillAnimationController skillAnimationController,
                IUnitGameObjectController unitGameObjectController,
                ICharacteristics characteristics,
                IAcolyteController acolyteController,
                IAgrController agrController,
                ICharacterMovementInfo characterMovementInfo,
                IUnityUpdateEvents unityUpdateEvents,
                Core.ILogger logger,
                ITargetUnitProvider targetUnitProvider)
        {
            var skillCaster = new SkillCaster(
                    characteristics,
                    unitGameObjectController,
                    acolyteController,
                    agrController,
                    TargetUnitRelation.Enemy);
            var achievements = Repository.Instance;
            var skillSoundPrefabProvider =
                    InstanceContainer.Instance.Resolve<ISkillSoundPrefabProvider>();
            var colliderActivator = new SkillColliderActivator(
                    GameObjectInstantiater,
                    unityUpdateEvents);
            var skillsProvider = new CharacterSkillsProvider(
                    skillCaster,
                    achievements,
                    new CharacterSkillPrefabProvider(),
                    skillSoundPrefabProvider,
                    colliderActivator,
                    unityUpdateEvents);
            var skillBehaviorProvider = new SkillBehaviorProvider(
                    skillCaster,
                    GameObjectInstantiater,
                    colliderActivator,
                    targetUnitProvider);
            
            return new CharacterWeapon(
                    skillBehaviorProvider,
                    skillAnimationController,
                    skillsProvider.GetExtendedSkills(),
                    skillsProvider.GetShapeSkills(),
                    skillsProvider.GetSealSkills(),
                    characterMovementInfo,
                    logger);
        }

        private LineRenderer GetLineRenderer()
        {
            var line = gameObject.GetComponent<LineRenderer>();
            //line.material = new Material(Shader.Find("Particles/Additive"));
            line.positionCount = 0;
            line.startWidth = 0.5f;
            line.endWidth = 0.5f;
            line.startColor = Color.green;
            line.endColor = Color.green;
            line.useWorldSpace = true;
            return line;
        }

        private IEnumerable<IShapeRecognizer> GetShapesRecognizers()
        {
            return new IShapeRecognizer[]
                   {
                           new DotRecognizer(),
                           new HorizontalLineRecognizer(),
                           new VerticalLineRecognizer(),
                           new ArcDownRecognizer(),
                           new ArcUpRecognizer(),
                           new CircleRecognizer()
                   };
        }
    }
}
