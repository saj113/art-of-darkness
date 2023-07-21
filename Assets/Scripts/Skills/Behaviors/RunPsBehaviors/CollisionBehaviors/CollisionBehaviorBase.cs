using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Skills.Particles;
using Stats;
using UnityEngine;

namespace Skills.Behaviors.RunPsBehaviors.CollisionBehaviors
{
    public abstract class CollisionBehaviorBase : ICollisionBehavior
    {
        private readonly IModificatorsApplier _modificatorsApplier;
        private readonly IGameObjectInstantiater _gameObjectInstantiater;
        private readonly SkillParticles _collisionParticles;
        private readonly bool _applyBuffs;

        protected CollisionBehaviorBase(
            IModificatorsApplier modificatorsApplier,
            IGameObjectInstantiater gameObjectInstantiater,
            SkillParticles collisionParticles,
            bool applyBuffs)
        {
            _modificatorsApplier = modificatorsApplier;
            _gameObjectInstantiater = gameObjectInstantiater;
            _collisionParticles = collisionParticles;
            _applyBuffs = applyBuffs;
        }

        public bool ForwardUnitCollision(IStats target, Vector2 position)
        {
            if (!IsUnitCollisionPossible(target)) return false;
            
            var collisionParticles =
                _gameObjectInstantiater.TryInstantiate(_collisionParticles, new Vector2(target.GameObjectController.Position.x, position.y));
            if (collisionParticles != null)
            {
                target.GameObjectController.AddChild(collisionParticles.transform);
            }
            
            _modificatorsApplier.ApplyModificators(target, _applyBuffs);

            return true;
        }

        public bool ForwardBarrierCollision(Tag colliderOwnerTag, Vector2 position)
        {
            if (!IsBarrierCollisionPossible(colliderOwnerTag)) return false;
            
            _gameObjectInstantiater.TryInstantiate(_collisionParticles, position);
            return true;
        }

        protected abstract bool IsUnitCollisionPossible(IStats target);
        protected abstract bool IsBarrierCollisionPossible(Tag colliderOwnerTag);
    }
}
