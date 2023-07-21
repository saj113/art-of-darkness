using System.Collections.Generic;
using Core;
using Core.Trigger;
using Core.UnityFramework;
using Skills.Parameters;
using Stats;
using UnityEngine;
using Utilities;

namespace Skills.Behaviors.SummonBehaviors.ColliderComponents
{
    public class AffectEveryTimeCollider : TemporaryBehaviorCollider
    {
        public float AffectCooldown = 0.5f;

        private float _affectTimeElapsed;

        private readonly IList<IStats> _targetsInCollision = 
            new List<IStats>();

        public AffectEveryTimeCollider(IGameObject collider, ISkillCaster caster, IColliderParameters parameters, ICollisionEvents collisionEvents, IGameObjectInstantiater instantiater, IUnityUpdateEvents updateEvents)
            : base(collider, caster, parameters, collisionEvents, instantiater, updateEvents)
        {
        }

        protected override void CollisionDetected(IStats target, Vector2 pos)
        {
            if (!ConditionUtility.CheckUnitRelationship(Caster, target, Parameters.TargetUnitRelation))
            {
                return;
            }

            _targetsInCollision.Add(target);
        }

        protected override void UpdateState()
        {
            if (_affectTimeElapsed >= AffectCooldown)
            {
                ExecuteTargetsInCollider();
                InstantiateParticles(Parameters.CollisionParticles);
                _affectTimeElapsed = 0.0f;
            }
            else
            {
                _affectTimeElapsed += Time.deltaTime;
            }
        }

        private void ExecuteTargetsInCollider()
        {
            foreach (var target in _targetsInCollision)
            {
                if (Collider.IsVectorOnCollider(target.GameObjectController.CenterPosition))
                {
                    Parameters.Modificators.ApplyAll(target);
                }
            }
        }
    }
}
