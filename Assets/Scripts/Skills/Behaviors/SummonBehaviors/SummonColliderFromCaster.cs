using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;

namespace Skills.Behaviors.SummonBehaviors
{
    public class SummonColliderFromCaster : SkillBehavior<ISummonColliderFromCasterParameters>
    {
        private readonly ISkillColliderActivator _colliderActivator;
        public SummonColliderFromCaster(
            ISkillCaster caster, 
            ISummonColliderFromCasterParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            ISkillColliderActivator colliderActivator) 
            : base(caster, parameters, gameObjectInstantiater)
        {
            _colliderActivator = colliderActivator;
        }

        protected override void ActivateCore()
        {
            _colliderActivator.Activate(Caster, Parameters.ColliderParameters);
        }
    }
}
