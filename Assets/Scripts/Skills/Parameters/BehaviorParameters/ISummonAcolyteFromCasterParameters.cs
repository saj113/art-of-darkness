using Skills.Behaviors;
using Skills.Modificators;
using Stats.Data;

namespace Skills.Parameters.BehaviorParameters
{
    public interface ISummonAcolyteFromCasterParameters : IBehaviorParameters
    {
        UnitStatsData[] Acolytes { get; }
    }

    public class SummonAcolyteFromCasterParameters : BehaviorParameters, ISummonAcolyteFromCasterParameters
    {
        public SummonAcolyteFromCasterParameters(
            SkillBehaviorType type,
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            AnimationSkillParticles animationParticles1,
            UnitStatsData[] acolytes)
            : base(type, animationParticles, modificators)
        {
            Acolytes = acolytes;
        }

        public UnitStatsData[] Acolytes { get; private set; }
    }
}
