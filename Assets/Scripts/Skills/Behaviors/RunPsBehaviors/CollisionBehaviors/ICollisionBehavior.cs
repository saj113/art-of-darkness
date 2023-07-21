using Stats;
using UnityEngine;

namespace Skills.Behaviors.RunPsBehaviors.CollisionBehaviors
{
    public interface ICollisionBehavior
    {
        bool ForwardUnitCollision(IStats target, Vector2 position);
        bool ForwardBarrierCollision(Tag colliderOwnerTag, Vector2 position);
    }
}
