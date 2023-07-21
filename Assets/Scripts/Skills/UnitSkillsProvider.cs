using System;
using Core;
using Core.Prefabs;
using Core.UnityFramework;
using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Cooldown;
using Skills.Modificators;
using Skills.Parameters;
using Skills.Parameters.BehaviorParameters;
using Skills.Particles;
using Stats;

namespace Skills
{
    public class UnitSkillsProvider : IUnitSkillsProvider
    {
        private readonly IUnitSkillPrefabProvider _unitSkillsProvider;
        private readonly ISkillCaster _caster;
        private readonly IUnityUpdateEvents _updateEvents;

        public UnitSkillsProvider(
            ISkillCaster caster,
            IUnitSkillPrefabProvider unitSkillsProvider,
            IUnityUpdateEvents updateEvents)
        {
            _caster = caster;
            _updateEvents = updateEvents;
            _unitSkillsProvider = unitSkillsProvider;
        }

        public ISkillParameters GetSkill(IUnitSkillInfo skillInfo)
        {
            switch (skillInfo.Name)
            {
                case UnitSkillName.Attack:
                    return GetAttackSkill(skillInfo);
                
                case UnitSkillName.WeakAttack:
                    return GetWeakAttackSkill(skillInfo);
                
                case UnitSkillName.FastAttack:
                    return GetFastAttackSkill(skillInfo);
                
                case UnitSkillName.StrongAttack:
                    return GetStrongAttackSkill(skillInfo);
                
                case UnitSkillName.Fireball1:
                    return GetFireball1Skill(skillInfo);
                    
                case UnitSkillName.Fireball2:
                    return GetFireball2Skill(skillInfo);

                case UnitSkillName.Summon:
                    break;
            }
            
            throw new NotSupportedException();
        }

        private ISkillParameters GetAttackSkill(IUnitSkillInfo skillInfo)
        {
            return GetAffectTargetSkill(
                skillInfo,
                2,
                1,
                100,
                1);
        }
        
        private ISkillParameters GetWeakAttackSkill(IUnitSkillInfo skillInfo)
        {
            return GetAffectTargetSkill(
                skillInfo,
                2,
                1,
                70,
                1);
        }

        private ISkillParameters GetFastAttackSkill(IUnitSkillInfo skillInfo)
        {
            return GetAffectTargetSkill(
                skillInfo,
                1,
                0.5f,
                70,
                1);
        }

        private ISkillParameters GetStrongAttackSkill(IUnitSkillInfo skillInfo)
        {
            return GetAffectTargetSkill(
                skillInfo,
                1.5f,
                1,
                150,
                1.5f);
        }

        private ISkillParameters GetFireball1Skill(IUnitSkillInfo skillInfo)
        {
            return GetParticlesFromCasterSkill(
                skillInfo,
                2f,
                0.6f, //TODO: 1
                100,
                20f,
                _unitSkillsProvider.GetFireball1(),
                _unitSkillsProvider.GetFireball1Collision());
        }

        private ISkillParameters GetFireball2Skill(IUnitSkillInfo skillInfo)
        {
            return GetParticlesFromCasterSkill(
                skillInfo,
                3f,
                0.6f, //TODO: 1
                0,
                30f,
                _unitSkillsProvider.GetFireball2(),
                _unitSkillsProvider.GetFireball2Collision());
        }
        
        private ISkillParameters GetAffectTargetSkill(
            IUnitSkillInfo skillInfo,
            float cooldown,
            float castTime,
            int power,
            float range)
        {
            var generalParameters = new GeneralParameters(
                new TimeCooldown(_updateEvents, cooldown),
                null,
                range: range);
            var animationParameters = new AnimationParameters(
                castTime,
                skillInfo.AnimationNames,
                null,
                skillInfo.Sound,
                false);
            var affectTargetParameters = new AffectToTargetParameters(
                null,
                new IModificator[] { new Damage(_caster, power) });

            return new SkillParameters(
                generalParameters,
                animationParameters,
                affectTargetParameters);
        }

        private ISkillParameters GetParticlesFromCasterSkill(
            IUnitSkillInfo skillInfo,
            float cooldown,
            float castTime,
            int power,
            float range,
            MovementSkillParticles movementSkillParticles,
            SkillParticles collisionParticles)
        {
            var generalParameters = new GeneralParameters(
                new TimeCooldown(_updateEvents, cooldown),
                null,
                range: range);
            var animationParameters = new AnimationParameters(
                castTime,
                skillInfo.AnimationNames,
                null,
                skillInfo.Sound,
                false);

            var modificators = new IModificator[]
            {
                new Damage(_caster, power)
            };

            var particlesFromCasterParameters = new ParticlesFromCasterParameters(
                null,
                modificators,
                TargetUnitRelation.Enemy,
                new MovementSkillParticlesParameters(
                    1,
                    movementSkillParticles),
                collisionParticles,
                skillInfo.DeviationFromCenter);
            
            return new SkillParameters(
                generalParameters,
                animationParameters,
                particlesFromCasterParameters);
        }
    }
}
