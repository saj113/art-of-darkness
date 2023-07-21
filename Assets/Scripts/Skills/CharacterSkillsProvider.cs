using System.Collections.Generic;
using Core;
using Core.Animation;
using Core.Prefabs;
using Core.UnityFramework;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Cooldown;
using Skills.Modificators;
using Skills.Modificators.Buffs;
using Skills.Parameters;
using Skills.Parameters.BehaviorParameters;
using Skills.Parameters.ModificatorParameters;
using Skills.Particles;
using Stats;
using Store;
using UnitControllers.AcolytesBehavior;
using UnitControllers.TouchControllers;
using UnityEngine;

namespace Skills
{
    public class CharacterSkillsProvider
    {
        private readonly ICharacterSkillPrefabProvider _characterSkillPrefabProvider;
        private readonly ISkillSoundPrefabProvider _skillSoundPrefabProvider;
        private readonly IRepository _repository;
        private readonly ISkillCaster _caster;
        private readonly ISkillColliderActivator _skillColliderActivator;
        private readonly IUnityUpdateEvents _updateEvents;

        public CharacterSkillsProvider(
            ISkillCaster caster,
            IRepository repository,
            ICharacterSkillPrefabProvider characterSkillPrefabProvider,
            ISkillSoundPrefabProvider skillSoundPrefabProvider,
            ISkillColliderActivator skillColliderActivator,
            IUnityUpdateEvents updateEvents)
        {
            _caster = caster;
            _characterSkillPrefabProvider = characterSkillPrefabProvider;
            _skillSoundPrefabProvider = skillSoundPrefabProvider;
            _repository = repository;
            _skillColliderActivator = skillColliderActivator;
            _updateEvents = updateEvents;
        }

        public ISkillParameters[] GetShapeSkills()
        {
            var result = new List<ISkillParameters>();

            if (_repository.IsTalentSelected(TalentNameKey.Magicka_Resurrect))
            {
                result.Add(GetRessurectSkill());
            }

            if (_repository.IsTalentSelected(TalentNameKey.Magicka_Shade))
            {
                result.Add(GetShadeSkill());
            }

            if (_repository.IsTalentSelected(TalentNameKey.Magicka_DrainLife))
            {
                result.Add(GetDrainLifeSkill());
            }

            return result.ToArray();
        }

        public IExtendedSkillParameters[] GetExtendedSkills()
        {
            var result = new List<IExtendedSkillParameters>();

            if (_repository.IsTalentSelected(TalentNameKey.Magicka_ShadowBolts))
            {
                result.Add(GetShadowBoltsSkill());
            }

            if (_repository.IsTalentSelected(TalentNameKey.Magicka_NecromanticSword))
            {
                result.Add(GetNecromancerSwordSkill());
            }

            if (_repository.IsTalentSelected(TalentNameKey.Magicka_ShadowJump))
            {
                result.Add(GetShadowJumpSkill());
            }

            return result.ToArray();
        }

        public IExtendedSkillParameters[] GetSealSkills()
        {
            var result = new List<IExtendedSkillParameters>();
            
            if (_repository.IsTalentSelected(TalentNameKey.Magicka_DeathSeal))
            {
                result.Add(GetDeathSealSkill());
            }
            
            return result.ToArray();
        }

        #region Character MAGICKA skills

        private IExtendedSkillParameters GetShadowBoltsSkill()
        {
            var generalParameters = new GeneralParameters(
                new TimeCooldown(_updateEvents, 1),
                _characterSkillPrefabProvider.GetShadowBoltSprite());
            var animationParameters = new AnimationParameters(
                0.6f, //TODO: 1
                AnimationConstants.Skill.Character.SummonAndStaffForward,
                null,
                _skillSoundPrefabProvider.GetShadowBoltsEventClip());

            var modificators = new IModificator[]
            {
                new Damage(_caster, 150),
                new ChangeCasterStat(
                    _caster,
                    TargetUnitRelation.Enemy,
                    StatAttribute.Mana,
                    2)
            };

            var particlesFromCasterParameters = new ParticlesFromCasterParameters(
                null,
                modificators,
                TargetUnitRelation.Enemy,
                new MovementSkillParticlesParameters(
                    2,
                    _characterSkillPrefabProvider.GetShadowBoltSkillParticles()),
                _characterSkillPrefabProvider.GetShadowBoltCollisionSkillParticles(),
                new Vector2(2f, 0));

            var heldSkill = GetDarkStreamSkill();
            return new ExtendedSkillParameters(
                generalParameters,
                animationParameters,
                particlesFromCasterParameters,
                heldSkill);
        }
        
        private IExtendedSkillParameters GetDeathSealSkill()
        {
            var generalParameters = new GeneralParameters(
                new TimeCooldown(_updateEvents, 20),
                _characterSkillPrefabProvider.GetShadeSprite());
            var animationParameters = new AnimationParameters(
                5f,
                AnimationConstants.Skill.Character.StaffUp,
                null,
                null);
            
            var colliderParameters = new ColliderParameters(
                ColliderBehaviorType.AffectWhileInAreaCollider,
                Tag.Player,
                TargetUnitRelation.Friendly,
                31,
                1,
                _characterSkillPrefabProvider.GetDeathSealCollider(),
                _characterSkillPrefabProvider.GetDeathSealParticles(),
                null,
                null,
                null,
                new IModificator[]
                {
                    new AddBuffModificator<IRegenBuffParameters>(
                        _caster,
                        TargetUnitRelation.PlayerOnly,
                        new RegenBuffParameters(30, null, 1, 100, BuffType.HoT)),
                    new AddBuffModificator<IRegenBuffParameters>(
                        _caster,
                        TargetUnitRelation.PlayerOnly,
                        new RegenBuffParameters(30, null, 1, 100, BuffType.MoT)),
                });

            var summonCollider = new SummonColliderOnTarget(
                _caster, _skillColliderActivator, TargetUnitRelation.PlayerOnly, colliderParameters);
            var devourCorpsesParameters = new DevourCorpsesParameters(
                null,
                new IModificator[1] { summonCollider},
                8,
                4,
                null);

            return new ExtendedSkillParameters(
                generalParameters,
                animationParameters,
                devourCorpsesParameters,
                null);
        }

        private IExtendedSkillParameters GetNecromancerSwordSkill()
        {
            var generalParameters = new GeneralParameters(
                new NoneCooldown(),
                _characterSkillPrefabProvider.GetNecromancerSwordSprite());
            var animations = new string[]
            {
                AnimationConstants.Skill.Character.AttackSword1,
                AnimationConstants.Skill.Character.AttackSword2,
                AnimationConstants.Skill.Character.AttackSword3
            };
            var animationParameters = new AnimationParameters(
                1f,
                animations,
                null,
                null,
                false);
            var modificators = new IModificator[]
            {
                new Damage(_caster, 50)
            };

            var rayFromCasterParameters = new RayFromCasterParameters(
                null,
                modificators,
                TargetUnitRelation.Enemy,
                2,
                Direction.Forward,
                3);

            return new ExtendedSkillParameters(
                generalParameters,
                animationParameters,
                rayFromCasterParameters,
                null);
        }

        private ISkillParameters GetDarkBarrierSkill()
        {
            var generalParameters = new GeneralParameters(new NoneCooldown());
            var animationParameters = new AnimationParameters(
                0.5f,
                AnimationConstants.Skill.Character.SummonAndStaffForward,
                null,
                null,
                true);
            var modificators = new IModificator[]
            {
                new HealCaster(_caster, TargetUnitRelation.Enemy, 20),
                new ChangeCasterStat(
                    _caster,
                    TargetUnitRelation.Enemy,
                    StatAttribute.Mana,
                    5)
            };
            var colliderParameters = new ColliderParameters(
                ColliderBehaviorType.Simple,
                Tag.Player,
                TargetUnitRelation.Enemy,
                9999,
                1,
                _characterSkillPrefabProvider.GetDarkBarrierCollider(),
                null,
                null,
                null,
                null,
                modificators);

            var summonSupportedColliderFromCasterParameters =
                new SummonColliderFromCasterParameters(
                    null,
                    new IModificator[0],
                    colliderParameters);

            return new SkillParameters(
                generalParameters,
                animationParameters,
                summonSupportedColliderFromCasterParameters);
        }

        private ISkillParameters GetDarkStreamSkill()
        {
            var generalParameters = new GeneralParameters(new NoneCooldown());
            var animationParameters = new AnimationParameters(
                0.5f,
                AnimationConstants.Skill.Character.SummonAndStaffForward,
                null,
                null,
                true);
            var modificators = new IModificator[]
            {
                new Damage(_caster, TargetUnitRelation.Enemy, 17),
                new ChangeCasterStat(
                    _caster,
                    TargetUnitRelation.Enemy,
                    StatAttribute.Mana,
                    1)
            };
            var rayFromCasterParameters = new RayFromCasterParameters(
                _characterSkillPrefabProvider.GetDarkStreamAnimationSkillParticles(),
                modificators,
                TargetUnitRelation.Enemy,
                5,
                Direction.Forward,
                20);

            return new SkillParameters(generalParameters, animationParameters, rayFromCasterParameters);
        }

        private ISkillParameters GetRessurectSkill()
        {
            var cooldowns = new ISkillCooldown[]
            {
                new ManaCooldown(_caster.Characteristics, 20),
                new HealthCooldown(_caster.Characteristics, 10)
            };
            var generalParameters = new GeneralParameters(
                cooldowns,
                shapeType: ShapeType.Dot,
                icon: _characterSkillPrefabProvider.GetRessurectSprite());
            var animationParameters = new AnimationParameters(
                3f,
                AnimationConstants.Skill.Character.Summon,
                null,
                null);
            var modificators = new IModificator[]
            {
                new AddBuffModificator<IEnslaveBuffParameters>(
                    _caster,
                    TargetUnitRelation.DeadOnly,
                    new EnslaveBuffParameters(AcolyteType.Zombie, 360)),
                new Heal(_caster, TargetUnitRelation.Friendly, 200),
                new DamageCaster(_caster, TargetUnitRelation.Friendly, 100)
            };
            var rayFromPointParameters = new RayFromPointParameters(
                null,
                modificators,
                null,
                TargetUnitRelation.DeadOnly,
                2,
                1);

            return new SkillParameters(generalParameters, animationParameters, rayFromPointParameters);
        }

        private ISkillParameters GetShadeSkill()
        {
            var generalParameters = new GeneralParameters(
                GetManaAndTimeCooldowns(20, 10),
                shapeType: ShapeType.LineHorizontal,
                icon: _characterSkillPrefabProvider.GetShadeSprite());
            var animationParameters =
                new AnimationParameters(
                    2f,
                    AnimationConstants.Skill.Character.StaffUp,
                    null,
                    null);
            var modificators = new IModificator[]
            {
                new AddBuffModificator<ISlowdownBuffParameters>(
                    _caster,
                    TargetUnitRelation.Enemy,
                    new SlowdownBuffParameters(60, 60))
            };
            var colliderParameters = new ColliderParameters(
                ColliderBehaviorType.AffectWhileInAreaCollider,
                Tag.Player,
                TargetUnitRelation.Enemy,
                15,
                1,
                _characterSkillPrefabProvider.GetShadeCollider(),
                _characterSkillPrefabProvider.GetShadeParticles(),
                null,
                null,
                null,
                modificators);

            var summonColliderFromPoint = new SummonColliderFromPointParameters(
                null,
                new IModificator[0],
                null,
                TargetUnitRelation.Enemy,
                colliderParameters);

            return new SkillParameters(generalParameters, animationParameters, summonColliderFromPoint);
        }

        private IExtendedSkillParameters GetShadowJumpSkill()
        {
            var cooldowns = GetManaAndTimeCooldowns(10, 10);
            var generalParameters = new GeneralParameters(
                cooldowns,
                icon: _characterSkillPrefabProvider.GetShadowJumpSprite());
            var animationParameters =
                new AnimationParameters(
                    0.2f,
                    AnimationConstants.Skill.Character.Summon,
                    null,
                    null);
            var modificators = new IModificator[]
            {
                new ChangePosition(
                    _caster,
                    TargetUnitRelation.PlayerOnly,
                    20,
                    Direction.Forward),
            };
            var affectToCasterParameters = new AffectToCasterParameters(null, modificators);

            return new ExtendedSkillParameters(
                generalParameters,
                animationParameters,
                affectToCasterParameters,
                null);
        }

        private ISkillParameters GetDrainLifeSkill()
        {
            var generalParameters = new GeneralParameters(
                new ManaCooldown(_caster.Characteristics, 20),
                icon: _characterSkillPrefabProvider.GetDrainLifeSprite(),
                shapeType: ShapeType.LineVertical);
            var animationParameters = new AnimationParameters(
                1.5f,
                AnimationConstants.Skill.Character.Summon2,
                null,
                null);
            var modificators = new IModificator[]
            {
                new Damage(_caster, TargetUnitRelation.Enemy, 80),
                new Heal(_caster, TargetUnitRelation.PlayerOnly, 50),
            };
            var behaviorParameters = new ParticlesToCasterFromTargetsParameters(
                null,
                modificators,
                null,
                TargetUnitRelation.Enemy,
                new MovementSkillParticlesParameters(
                    1,
                    _characterSkillPrefabProvider
                        .GetDrainLifeSkillParticles()),
                _characterSkillPrefabProvider
                    .GetDrainLifeCollisionSkillParticles(),
                5,
                3);

            return new SkillParameters(
                generalParameters,
                animationParameters,
                behaviorParameters);
        }

        #endregion

        private ISkillCooldown[] GetManaAndTimeCooldowns(int requiredMana, float recoveryTime)
        {
            return new ISkillCooldown[]
            {
                new ManaCooldown(_caster.Characteristics, requiredMana),
                new TimeCooldown(_updateEvents, recoveryTime), 
            };
        }
    }
}
