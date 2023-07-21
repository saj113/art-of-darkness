using Skills.Behaviors;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnityEngine;

namespace Skills
{
    public interface ISkillBehaviorProvider
    {
        IBehaviorActivatable GetSkillBehavior(IBehaviorParameters behaviorParameters);
        IBehaviorActivatable GetSkillBehavior(IBehaviorParameters behaviorParameters, IStats target);
        IBehaviorActivatable GetSkillBehaviorToPointComponent(IBehaviorParameters behaviorParameters, float targetPosition);
    }
}