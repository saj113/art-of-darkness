using System;

namespace Skills
{
    public enum RelationSide { Default, PlayerSide, EnemySide}
    public enum TargetUnitRelation { None, Friendly, Enemy, PlayerOnly, DeadOnly}
    public enum SkillBehaviorType
    {
        ParticlesFromCaster,
        ParticlesFromCasterSequentially,
        ParticlesToCaster,
        RayFromCaster,
        SummonAcolyteFromCaster,
        SummonColliderFromCaster,
        SummonColliderFromPoint,
        AffectToCaster,
        RayFromPoint,
        AffectToTarget,
        ParticlesToTarget,
        ParticlesFromSky,
        ParticlesToCasterFromTargets,
        SummonSupportedColliderFromCaster,
        DevourCorpses
    }

    public enum ShapeSkillBehaviorType
    {
        ParticlesToTarget,
    }

    public enum ModificatorType
    {
        Damage = 0,
        RangeDamage = 1,
        Attract = 2,
        Heal = 3,
        Throwing = 4,
        SummonCollider = 5,
        AddAbsorbShieldBuff = 6,
        AddChangeStatsBuff = 7,
        AddDoTBuff = 8,
        AddEnslaveBuff = 9,
        AddFearBuff = 10,
        AddHoTBuff = 11,
        AddSlowdownBuff = 12,
        AddStunBuff = 13,
        DamageCaster = 14,
        AddChangeTagBuff = 15,
        HealCaster = 16,
        AddFlyBuff = 17,
        DamageByPercent = 18,
        ChangeCasterStat = 19,
        ChangePosition = 20
    }

    public enum ColliderBehaviorType
    {
        AffectEveryTimeCollider,
        AffectOnFirstTriggerEnterCollider,
        AffectWhileInAreaCollider,
        Simple
    }
    
    public enum SkillCooldownType {
        None,
        Time,
        Mana,
        Health
    }

    public enum SkillUseFailedReason
    {
        None,
        TimeCooldown,
        ManaNotEnough,
        HealthNotEnough,
        CorpsesNotEnoughInPlace,
        CorpsesNotEnoughNearby,
        TargetsNotFound,
        ShapeNotRecognized,
        StateIsInvalid
    }

    // ReSharper disable all InconsistentNaming
    public enum TalentNameKey
    {
        Magicka_ShadowBolts = 0,
        Magicka_ShadowBolts_Increase_Generation_Mana = 1,
        Magicka_ShadowBolts_Reduce_Cooldown = 2,
        Magicka_ShadowBolts_Increase_Bolts_Count = 3,
        Magicka_ShadowBolts_Bonus = 4,
        Magicka_NecromanticSword = 5,
        Magicka_NecromanticSword1= 6,
        Magicka_NecromanticSword2 = 7,
        Magicka_NecromanticSword3 = 8,
        Magicka_NecromanticSword4 = 9,
        Magicka_Resurrect = 10,
        Magicka_Resurrect1 = 11,
        Magicka_Resurrect2 = 12,
        Magicka_Resurrect3 = 13,
        Magicka_Resurrect4 = 14,
        Magicka_Shade = 15,
        Magicka_Shade1 = 16,
        Magicka_Shade2 = 17,
        Magicka_Shade3 = 18,
        Magicka_Shade4 = 19,
        Magicka_NecroticShield = 20,
        Magicka_NecroticShield1 = 21,
        Magicka_NecroticShield2 = 22,
        Magicka_NecroticShield3 = 23,
        Magicka_NecroticShield4 = 24,
        Magicka_NecroticShield5 = 25,
        Magicka_BoneSpear = 26,
        Magicka_ShadowJump = 27,
        Magicka_DrainLife = 28,
        Magicka_Enslave = 29,
        Magicka_DarkStream = 29,
        Magicka_DeathSeal = 30,
    }

    public enum UnitSkillName
    {
        Attack,
        FastAttack,
        StrongAttack,
        Fireball1,
        Fireball2,
        Summon,
        WeakAttack,
    }
}
