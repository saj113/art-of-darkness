using Core;
using Skills.Parameters.BehaviorParameters;

namespace Skills.Parameters
{
    public class ExtendedSkillParameters : SkillParameters, IExtendedSkillParameters
    {
        public ISkillParameters HeldSkillParameters { get; private set; }

        public ExtendedSkillParameters(IGeneralParameters general, IAnimationParameters animation, IBehaviorParameters behaviorParameters, ISkillParameters heldSkillParameters)
            : base(general, animation, behaviorParameters)
        {
            HeldSkillParameters = heldSkillParameters;
            if (HeldSkillParameters != null)
            {
                Contract.Ensure(HeldSkillParameters.Animation.IsLoop, "HeldSkillParameters.Animation.IsLoop is FALSE");
            }
        }
    }
}