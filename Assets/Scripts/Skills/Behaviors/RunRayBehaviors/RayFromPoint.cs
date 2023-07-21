using System.Linq;
using Core.Provider;
using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnityEngine;

namespace Skills.Behaviors.RunRayBehaviors
{
    public class RayFromPoint : SkillBehaviorToPoint<IRayFromPointParameters>
    {
        public RayFromPoint(
            ISkillCaster caster,
            IRayFromPointParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            ITargetUnitProvider targetUnitProvider,
            float targetPosition)
            : base(caster, parameters, gameObjectInstantiater, targetUnitProvider, targetPosition)
        {
        }

        protected override void ActivateCore()
        {
            var units = GetUnits(TargetPosition);
            foreach (var target in units)
            {
                Parameters.ApplyModificators(target, !IsActivated);
            }
        }

        public override bool IsActivatable(out SkillUseFailedReason failedReason)
        {
            var result = GetUnits(TargetPosition,
                    Parameters.TargetUnitRelation,
                    Parameters.Distance,
                    Parameters.MaxTargetCount)
                .Any();
            failedReason = result ? SkillUseFailedReason.None : SkillUseFailedReason.TargetsNotFound;
            return result;
        }
    }
}
