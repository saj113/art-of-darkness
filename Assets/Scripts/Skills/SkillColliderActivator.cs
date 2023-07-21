using System;
using Core;
using Core.Provider;
using Core.Trigger;
using Core.UnityFramework;
using Skills.Behaviors.SummonBehaviors.ColliderComponents;
using Skills.Parameters;
using Stats;
using UnityEngine;

namespace Skills
{
    public interface ISkillColliderActivator
    {
        IBehaviorCollider Activate(
                ISkillCaster caster,
                IColliderParameters colliderParameters,
                float xPosition);

        IBehaviorCollider Activate(ISkillCaster caster, IColliderParameters colliderParameters);
    }

    public class SkillColliderActivator : ISkillColliderActivator
    {
        private readonly IGameObjectInstantiater _gameObjectInstantiater;
        private readonly IUnityUpdateEvents _updateEvents;

        public SkillColliderActivator(
                IGameObjectInstantiater gameObjectInstantiater,
                IUnityUpdateEvents unityUpdateEvents)
        {
            _gameObjectInstantiater = gameObjectInstantiater;
            _updateEvents = unityUpdateEvents;
        }

        public IBehaviorCollider Activate(
                ISkillCaster caster,
                IColliderParameters colliderParameters,
                float xPosition)
        {
            return CreateCollider(caster, colliderParameters, xPosition);
        }

        public IBehaviorCollider Activate(
                ISkillCaster caster,
                IColliderParameters colliderParameters)
        {
            return CreateCollider(
                    caster,
                    colliderParameters,
                    caster.GameObjectController.CenterPosition.x);
        }

        private IBehaviorCollider CreateCollider(
                ISkillCaster caster,
                IColliderParameters colliderParameters,
                float xPosition)
        {
            var collider = _gameObjectInstantiater.Instantiate(
                    colliderParameters.ColliderPrefab,
                    new Vector3(xPosition, 0));
            /*foreach (var child in collider.gameObject.GetComponentsInChildren<Transform>())
            {
                child.transform.localScale = new Vector2(
                    colliderParameters.ColliderScale, colliderParameters.ColliderScale);
            }*/
            collider.gameObject.transform.localScale = new Vector2(
                    colliderParameters.ColliderScale,
                    colliderParameters.ColliderScale);
            
            var behaviorCollider = InitializeCollider(collider, caster, colliderParameters);

            return behaviorCollider;
        }

        private IBehaviorCollider InitializeCollider(
                Collider2D collider,
                ISkillCaster caster,
                IColliderParameters colliderParameters)
        {
            var collisionEvents = new CollisionEvents(
                colliderParameters.OwnerTag,
                colliderParameters.TargetUnitRelation,
                collider);
            switch (colliderParameters.Type)
            {
                case ColliderBehaviorType.AffectEveryTimeCollider:
                    return new AffectEveryTimeCollider(
                            new UnityGameObject(collider.gameObject),
                            caster,
                            colliderParameters,
                            collisionEvents,
                            _gameObjectInstantiater,
                            _updateEvents);

                case ColliderBehaviorType.AffectOnFirstTriggerEnterCollider:
                    return new AffectOnFirstTriggerEnterCollider(
                            new UnityGameObject(collider.gameObject),
                            caster,
                            colliderParameters,
                            collisionEvents,
                            _gameObjectInstantiater,
                            _updateEvents);

                case ColliderBehaviorType.AffectWhileInAreaCollider:
                    return new AffectWhileInAreaCollider(
                            new UnityGameObject(collider.gameObject),
                            caster,
                            colliderParameters,
                            collisionEvents,
                            _gameObjectInstantiater,
                            _updateEvents);

                case ColliderBehaviorType.Simple:
                    return new SimpleBehaviorCollider(
                            new UnityGameObject(collider.gameObject),
                            caster,
                            colliderParameters,
                            collisionEvents,
                            _gameObjectInstantiater,
                            _updateEvents);

                default: throw new NotSupportedException();
            }
        }

        #region legacy creation multiply colliders
        /*
        public static void Activate(ISkillCaster caster, IColliderParameters colliderParameters)
        {
            var objectPositionDistance = 0.0f;
            for (var i = 0; i < colliderParameters.SummonCount; i++)
            {
                var collider = CreateCollider(caster, colliderParameters);
                float summonPositionX;
                if (colliderParameters.Direction == Direction.Round)
                {
                    summonPositionX = GetStartPositionXForRoundDirection(
                        ref objectPositionDistance, caster.Position.x, colliderParameters);
                }
                else
                {
                    objectPositionDistance += colliderParameters.Distance + colliderParameters.ColliderSize / 2;

                    summonPositionX = colliderParameters.Direction == Direction.Back
                        ? caster.Position.x - objectPositionDistance * ValueUtility.GetDirection(caster)
                        : caster.Position.x + objectPositionDistance * ValueUtility.GetDirection(caster);
                }

                collider.transform.position = new Vector2(summonPositionX, 0);
            }
        }
        private static float GetStartPositionXForRoundDirection(
            ref float objectPositionDistance, float casterPosition, IColliderParameters colliderParameters)
        {
            if (objectPositionDistance >= 0)
            {
                objectPositionDistance += colliderParameters.Distance + colliderParameters.ColliderSize / 2;
            }

            var summonPositionX = casterPosition + objectPositionDistance;
            objectPositionDistance *= -1;

            return summonPositionX;
        }
        */
        #endregion
    }
}
