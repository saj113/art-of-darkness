using Core;
using Core.Trigger;
using Core.UnityFramework;
using Skills.Parameters;
using Stats;
using UnityEngine;
using Utilities;

namespace Skills.Behaviors.SummonBehaviors.ColliderComponents
{
    public class AffectOnFirstTriggerEnterCollider : TemporaryBehaviorCollider
    {
        public AffectOnFirstTriggerEnterCollider(IGameObject collider, ISkillCaster caster, IColliderParameters parameters, ICollisionEvents collisionEvents, IGameObjectInstantiater instantiater, IUnityUpdateEvents updateEvents)
            : base(collider, caster, parameters, collisionEvents, instantiater, updateEvents)
        {
        }

        protected override void CollisionDetected(IStats target, Vector2 pos)
        {
            if (!ConditionUtility.CheckUnitRelationship(Caster, target, Parameters.TargetUnitRelation))
            {
                return;
            }

            Parameters.Modificators.ApplyAll(target);
            InstantiateParticles(Parameters.CollisionParticles);
        }
    }
}
