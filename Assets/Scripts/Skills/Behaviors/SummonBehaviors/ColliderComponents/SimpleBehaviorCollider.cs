using Core;
using Core.Trigger;
using Core.UnityFramework;
using Skills.Parameters;
using Stats;

namespace Skills.Behaviors.SummonBehaviors.ColliderComponents
{
    public class SimpleBehaviorCollider : BehaviorCollider
    {
        public SimpleBehaviorCollider(
            IGameObject collider, ISkillCaster caster, IColliderParameters parameters, 
            ICollisionEvents collisionEvents, IGameObjectInstantiater instantiater,
            IUnityUpdateEvents updateEvents) 
            : base(collider, caster, parameters, collisionEvents, instantiater, updateEvents)
        {
        }
    }
}