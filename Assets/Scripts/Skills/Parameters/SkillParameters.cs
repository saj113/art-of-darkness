using Skills.Parameters.BehaviorParameters;

namespace Skills.Parameters
{
    public class SkillParameters : ISkillParameters
    {
        public SkillParameters(
            IGeneralParameters general, 
            IAnimationParameters animation,
            IBehaviorParameters behaviorParameters)
        {
            General = general;
            Animation = animation;
            BehaviorParameters = behaviorParameters;
        }

        public IGeneralParameters General { get; private set; }
        public IAnimationParameters Animation { get; private set; }
        public IBehaviorParameters BehaviorParameters { get; private set; }

        public void Dispose()
        {
            General.Dispose();
        }
    }
}
