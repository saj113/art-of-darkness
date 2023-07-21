using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnitControllers.AcolytesBehavior;
using UnityEngine;

namespace Skills.Behaviors.SummonBehaviors
{
    public class SummonAcolyteFromCaster : SkillBehavior<ISummonAcolyteFromCasterParameters>
    {
        public SummonAcolyteFromCaster(ISkillCaster caster, ISummonAcolyteFromCasterParameters parameters, IGameObjectInstantiater gameObjectInstantiater)
            : base(caster, parameters, gameObjectInstantiater)
        {
        }

        protected override void ActivateCore()
        {
            foreach (var acolyte in Parameters.Acolytes)
            {
                var unit = Object.Instantiate(acolyte).Stats;
                unit.GameObjectController.Position = Caster.GameObjectController.Position;
                Caster.AcolyteController.AddAcolyte(unit, AcolyteType.Summon);
            }
        }
    }
}
