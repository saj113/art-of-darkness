using Core.Trigger;
using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Particles;
using UnityEngine;

namespace Core.Prefabs
{
    public interface ICharacterSkillPrefabProvider
    {
        MovementSkillParticles GetShadowBoltSkillParticles();
        MovementSkillParticles GetPillarOfHorrorSkillParticles();
        MovementSkillParticles GetDrainLifeSkillParticles();
        SkillParticles GetShadowBoltCollisionSkillParticles();
        SkillParticles GetDrainLifeCollisionSkillParticles();
        SkillParticles GetShadeParticles();
        SkillParticles GetDeathSealParticles();
        SkillParticles GetDarkShieldParticles();
        Collider2D GetShadeCollider();
        Collider2D GetDeathSealCollider();
        Collider2D GetDarkBarrierCollider();
        AnimationSkillParticles GetDarkStreamAnimationSkillParticles();
        Sprite GetShadowBoltSprite();
        Sprite GetNecromancerSwordSprite();
        Sprite GetRessurectSprite();
        Sprite GetShadowJumpSprite();
        Sprite GetShadeSprite();
        Sprite GetDrainLifeSprite();
        Sprite GetPillarOfHorrorSprite();
    }
}