using Core;
using Core.Trigger;
using Skills.Behaviors;
using Skills.Modificators;
using Skills.Particles;
using Stats;
using UnityEngine;

namespace Skills.Parameters
{
    public interface IColliderParameters
    {
        ColliderBehaviorType Type { get; }
        Tag OwnerTag { get; }
        TargetUnitRelation TargetUnitRelation { get; }
        float TimeDuration { get; }
        float ColliderScale { get; }
        Collider2D ColliderPrefab { get; }
        SkillParticles ColliderParticles { get; }
        SkillParticles ColliderCreateParticles { get; }
        SkillParticles ColliderDestroyParticles { get; }
        SkillParticles CollisionParticles { get; }
        IModificator[] Modificators { get; }
    }
}