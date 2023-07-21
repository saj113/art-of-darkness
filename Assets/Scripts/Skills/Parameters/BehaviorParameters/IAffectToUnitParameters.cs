using Skills.Behaviors;
using Skills.Modificators;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IAffectToUnitParameters : IBehaviorParameters
    {
    }

    public class AffectToCasterParameters : BehaviorParameters, IAffectToUnitParameters
    {
        public AffectToCasterParameters(
            AnimationSkillParticles animationParticles,
            IModificator[] modificators)
            : base(SkillBehaviorType.AffectToCaster, animationParticles, modificators)
        {
        }
    }
    
    public class AffectToTargetParameters : BehaviorParameters, IAffectToUnitParameters
    {
        public AffectToTargetParameters(
            AnimationSkillParticles animationParticles,
            IModificator[] modificators)
            : base(SkillBehaviorType.AffectToTarget, animationParticles, modificators)
        {
        }
    }
}
