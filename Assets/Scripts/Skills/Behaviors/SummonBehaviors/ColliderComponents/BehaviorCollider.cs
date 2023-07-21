using Core;
using Core.Trigger;
using Core.UnityFramework;
using Skills.Parameters;
using Skills.Particles;
using Stats;
using UnityEngine;

namespace Skills.Behaviors.SummonBehaviors.ColliderComponents
{
    public abstract class BehaviorCollider : IBehaviorCollider
    {
        private readonly IGameObjectInstantiater _instantiater;
        private readonly ICollisionEvents _collisionEvents;
        private readonly SkillParticles _colliderSkillParticles;
        private readonly IUnityUpdateEvents _updateEvents;
        protected BehaviorCollider(
            IGameObject collider,
            ISkillCaster caster, 
            IColliderParameters parameters,
            ICollisionEvents collisionEvents,
            IGameObjectInstantiater instantiater,
            IUnityUpdateEvents updateEvents)
        {
            Collider = collider;
            Collider.Destroyed += Dispose;
            Caster = caster;
            Parameters = parameters;
            _collisionEvents = collisionEvents;
            _instantiater = instantiater;
            _colliderSkillParticles = InstantiateParticles(Parameters.ColliderParticles);
            _updateEvents = updateEvents;
            InstantiateParticles(Parameters.ColliderCreateParticles);

            _collisionEvents.UnitCollisionEntered += CollisionDetected;
            _collisionEvents.UnitCollisionExited += OnCollisionExited;
            _collisionEvents.ProjectileBarrierTriggered += OnProjectileBarrierTriggered;
            _updateEvents.EverySecond += UpdateEventsOnEverySecond;
        }

        protected IColliderParameters Parameters { get; private set; }
        protected ISkillCaster Caster { get; private set; }
        protected IGameObject Collider { get; private set; }

        public virtual void Dispose()
        {
            if (_colliderSkillParticles != null)
            {
                _colliderSkillParticles.StopEmission();
            }

            InstantiateParticles(Parameters.ColliderDestroyParticles);
            Collider.Destroy();
            
            _collisionEvents.UnitCollisionEntered -= CollisionDetected;
            _collisionEvents.UnitCollisionExited -= OnCollisionExited;
            _collisionEvents.ProjectileBarrierTriggered -= OnProjectileBarrierTriggered;
            _updateEvents.EverySecond -= UpdateEventsOnEverySecond;
        }

        public void Rotate()
        {
            Collider.Rotatetion(0, 180, 0);
        }

        protected virtual void CollisionDetected(IStats target, Vector2 pos)
        {
        }

        protected virtual void OnCollisionExited(IStats target, Vector2 pos)
        {
        }
        protected virtual void OnProjectileBarrierTriggered(Tag colliderOwnerTag, Vector2 pos)
        {
        }

        protected SkillParticles InstantiateParticles(SkillParticles ps)
        {
            if (ps == null)
            {
                return null;
            }

            var instance = _instantiater.Instantiate(ps);
            instance.transform.position = new Vector2(Collider.Position.x, 0);
            instance.transform.localScale = new Vector2(Parameters.ColliderScale, Parameters.ColliderScale);
            return instance;
        }

        private void UpdateEventsOnEverySecond()
        {
            _collisionEvents.Update();
        }
    }
}
