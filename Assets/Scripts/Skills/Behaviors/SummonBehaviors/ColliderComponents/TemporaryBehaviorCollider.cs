using Core;
using Core.Trigger;
using Core.UnityFramework;
using Skills.Parameters;
using Stats;
using UnityEngine;

namespace Skills.Behaviors.SummonBehaviors.ColliderComponents
{
    public class TemporaryBehaviorCollider : BehaviorCollider
    {
        private float _timeElapsed;
        private readonly IUnityUpdateEvents _updateEvents;
        public TemporaryBehaviorCollider(
            IGameObject collider,
            ISkillCaster caster,
            IColliderParameters parameters, 
            ICollisionEvents collisionEvents, 
            IGameObjectInstantiater instantiater,
            IUnityUpdateEvents updateEvents)
            : base(collider, caster, parameters, collisionEvents, instantiater, updateEvents)
        {
            _updateEvents = updateEvents;
            _updateEvents.FixedUpdateFired += OnFixedUpdateFired;
        }

        public override void Dispose()
        {
            _updateEvents.FixedUpdateFired -= OnFixedUpdateFired;
            base.Dispose();
        }

        protected virtual void UpdateState()
        {

        }

        private void OnFixedUpdateFired(float deltaTime)
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed > Parameters.TimeDuration)
            {
                Dispose();
            }
            else
            {
                UpdateState();
            }
        }
    }
}
