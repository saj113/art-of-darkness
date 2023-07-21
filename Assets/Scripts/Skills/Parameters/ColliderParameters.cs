using Core;
using Core.Trigger;
using Skills.Behaviors;
using Skills.Modificators;
using Skills.Particles;
using Stats;
using UnityEngine;

namespace Skills.Parameters
{
    public class ColliderParameters : IColliderParameters
    {
        public ColliderParameters(
            ColliderBehaviorType type,
            Tag ownerTag,
            TargetUnitRelation targetUnitRelation,
            float timeDuration,
            float particlesScaleX,
            Collider2D сolliderPrefab,
            SkillParticles colliderParticles,
            SkillParticles colliderCreateParticles,
            SkillParticles colliderDestroyParticles,
            SkillParticles collisionParticles,
            IModificator[] modificators)
        {
            Type = type;
            OwnerTag = ownerTag;
            TargetUnitRelation = targetUnitRelation;
            TimeDuration = timeDuration;
            ColliderScale = particlesScaleX;
            ColliderPrefab = сolliderPrefab;
            ColliderParticles = colliderParticles;
            ColliderCreateParticles = colliderCreateParticles;
            ColliderDestroyParticles = colliderDestroyParticles;
            CollisionParticles = collisionParticles;
            Modificators = modificators;
        }

        public ColliderBehaviorType Type { get; private set; }
        public Tag OwnerTag { get; private set; }
        public TargetUnitRelation TargetUnitRelation { get; private set; }
        public float TimeDuration { get; private set; }
        public float ColliderScale { get; private set; }
        public Collider2D ColliderPrefab { get; private set; }
        public SkillParticles ColliderParticles { get; private set; }
        public SkillParticles ColliderCreateParticles { get; private set; }
        public SkillParticles ColliderDestroyParticles { get; private set; }
        public SkillParticles CollisionParticles { get; private set; }
        public IModificator[] Modificators { get; private set; }
    }
}
