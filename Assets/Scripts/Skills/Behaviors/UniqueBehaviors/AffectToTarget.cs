using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;

namespace Skills.Behaviors.UniqueBehaviors
{
    public class AffectToTarget : SkillBehavior<IAffectToUnitParameters>
    {
        private readonly IStats _target;

        public AffectToTarget(ISkillCaster caster,
            IAffectToUnitParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            IStats target)
            : base(caster, parameters, gameObjectInstantiater)
        {
            _target = target;
        }

        protected override void ActivateCore()
        {
            Parameters.ApplyModificators(_target, !IsActivated);
        }

    }
}
