using Core.Trigger;
using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Particles;
using UnityEngine;

namespace Core.Prefabs
{
    public class CharacterSkillPrefabProvider : PrefabProvider, ICharacterSkillPrefabProvider
    {
        public MovementSkillParticles GetShadowBoltSkillParticles()
        {
            return GetPrefab<MovementSkillParticles>("Particles/ShadowBolt");
        }

        public MovementSkillParticles GetPillarOfHorrorSkillParticles()
        {
            return GetPrefab<MovementSkillParticles>("Particles/PillarOfHorror");
        }

        public MovementSkillParticles GetDrainLifeSkillParticles()
        {
            return GetPrefab<MovementSkillParticles>("Particles/DrainLife");
        }

        public SkillParticles GetShadowBoltCollisionSkillParticles()
        {
            return GetPrefab<SkillParticles>("Particles/ShadowBoltCollision");
        }

        public SkillParticles GetShadeParticles()
        {
            return GetPrefab<SkillParticles>("Particles/ShadeParticles");
        }

        public SkillParticles GetDeathSealParticles()
        {            
            return GetPrefab<SkillParticles>("Particles/DeathSealParticles");
        }

        public SkillParticles GetDarkShieldParticles()
        {
            return GetPrefab<SkillParticles>("Particles/DarkShieldParticles");
        }

        public SkillParticles GetDrainLifeCollisionSkillParticles()
        {
            return GetPrefab<SkillParticles>("Particles/DrainLifeCollision");
        }

        public Sprite GetShadowBoltSprite()
        {
            return GetPrefab<Sprite>("Sprites/ShadowBolts");
        }

        public Sprite GetNecromancerSwordSprite()
        {
            return GetPrefab<Sprite>("Sprites/NecromancerSword");
        }

        public Sprite GetShadowJumpSprite()
        {
            return GetPrefab<Sprite>("Sprites/ShadowJump");
        }

        public AnimationSkillParticles GetDarkStreamAnimationSkillParticles()
        {
            return GetPrefab<AnimationSkillParticles>("Particles/DarkStream");
        }

        public Collider2D GetShadeCollider()
        {
            return GetPrefab<Collider2D>("Colliders/ShadeCollider");
        }

        public Collider2D GetDeathSealCollider()
        {
            return GetPrefab<Collider2D>("Colliders/DeathSealCollider");
        }

        public Sprite GetShadeSprite()
        {
            return GetPrefab<Sprite>("Sprites/Shade");
        }

        public Sprite GetDrainLifeSprite()
        {
            return GetPrefab<Sprite>("Sprites/DrainLife");
        }

        public Sprite GetPillarOfHorrorSprite()
        {
            return GetPrefab<Sprite>("Sprites/PillarOfHorror");
        }

        public Sprite GetRessurectSprite()
        {
            return GetPrefab<Sprite>("Sprites/Ressurect");
        }

        public Collider2D GetDarkBarrierCollider()
        {
            return GetPrefab<Collider2D>("Colliders/DarkBarrierCollider");
        }
    }
}