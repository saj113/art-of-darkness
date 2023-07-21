using System.Linq;
using Core.Provider;
using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;

namespace Skills.Behaviors.RunRayBehaviors
{
    public class RayFromCaster : SkillBehavior<IRayFromCasterParameters>
    {
        private readonly ITargetUnitProvider _targetUnitProvider;
        public RayFromCaster(ISkillCaster caster,
            IRayFromCasterParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            ITargetUnitProvider targetUnitProvider)
            : base(caster, parameters, gameObjectInstantiater)
        {
            _targetUnitProvider = targetUnitProvider;
        }

        protected override void ActivateCore()
        {
            var units = _targetUnitProvider.Get(Caster.Characteristics,
                                                Parameters.TargetUnitRelation,
                                                Caster.GameObjectController.Bounds,
                                                Parameters.Direction,
                                                Parameters.Distance)
                                           .Take(Parameters.MaxTargetCount);
            foreach (var target in units)
            {
                Parameters.ApplyModificators(target, !IsActivated);
            }
        }
    }
}
