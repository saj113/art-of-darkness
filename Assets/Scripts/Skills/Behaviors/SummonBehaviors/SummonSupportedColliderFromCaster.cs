using Core.UnityFramework;
using Skills.Behaviors.SummonBehaviors.ColliderComponents;
using Skills.Parameters.BehaviorParameters;
using Stats;

namespace Skills.Behaviors.SummonBehaviors
{
    public class SummonSupportedColliderFromCaster: SkillBehavior<ISummonSupportedColliderFromCasterParameters>
    {
        private readonly ISkillColliderActivator _colliderActivator;
        private IBehaviorCollider _projectileBarrierCollider; 
        public SummonSupportedColliderFromCaster(
            ISkillCaster caster, 
            ISummonSupportedColliderFromCasterParameters parameters, 
            IGameObjectInstantiater gameObjectInstantiater,
            ISkillColliderActivator colliderActivator) 
            : base(caster, parameters, gameObjectInstantiater)
        {
            _colliderActivator = colliderActivator;
        }

        public override void FinishActivation()
        {
            base.FinishActivation();
            _projectileBarrierCollider?.Dispose();
        }

        protected override void ActivateCore()
        {
            if (_projectileBarrierCollider != null)
            {
                return;
            }

            if (Caster.Characteristics.IsFacingRight)
            {
                _projectileBarrierCollider = _colliderActivator.Activate(
                    Caster, Parameters.ColliderParameters, Caster.GameObjectController.Position.x + Parameters.Distance);
            }
            else
            {
                _projectileBarrierCollider = _colliderActivator.Activate(
                    Caster, Parameters.ColliderParameters, Caster.GameObjectController.Position.x - Parameters.Distance);
                _projectileBarrierCollider.Rotate();
            }
            
        }
    }
}