using Core.Provider;
using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnityEngine;

namespace Skills.Behaviors.SummonBehaviors
{
    public class SummonColliderFromPoint : SkillBehaviorToPoint<ISummonColliderFromPointParameters>
    {
        private readonly ISkillColliderActivator _colliderActivator;
        public SummonColliderFromPoint(
            ISkillCaster caster,
            ISummonColliderFromPointParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            ITargetUnitProvider targetUnitProvider,
            ISkillColliderActivator colliderActivator,
            float targetPosition)
            : base(caster, parameters, gameObjectInstantiater, targetUnitProvider, targetPosition)
        {
            _colliderActivator = colliderActivator;
        }

        protected override void ActivateCore()
        {
            _colliderActivator.Activate(Caster, Parameters.ColliderParameters, TargetPosition);
        }
    }
}
