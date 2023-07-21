using Core;
using Core.Provider;
using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;

namespace Skills.Behaviors.UniqueBehaviors
{
    public class AffectToCaster : SkillBehavior<IAffectToUnitParameters>
    {
        private readonly ITargetUnitProvider _targetUnitProvider;
        public AffectToCaster(
            ISkillCaster caster,
            IAffectToUnitParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            ITargetUnitProvider targetUnitProvider)
            : base(caster, parameters, gameObjectInstantiater)
        {
            _targetUnitProvider = targetUnitProvider;
        }

        protected override void ActivateCore()
        {
            Parameters.ApplyModificators(
                    _targetUnitProvider.Get(Caster.Characteristics.Tag, Caster.GameObjectController.Position),
                    !IsActivated);
        }
    }
}
