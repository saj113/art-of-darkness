using Skills.Behaviors.RunPsBehaviors;
using Skills.Behaviors.RunPsBehaviors.CollisionBehaviors;
using Stats;
using UnityEngine;

namespace Skills.Particles
{
    public interface IMovementSkillParticles
    {
        void Initialize(Tag ownerTag, ParticlesTarget particlesTarget, ICollisionBehavior collisionBehavior, Vector2 startPosition);
    }
}