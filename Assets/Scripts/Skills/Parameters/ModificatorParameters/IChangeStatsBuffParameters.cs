using Skills.Behaviors;
using Skills.Particles;
using Stats;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IChangeStatsBuffParameters : IBuffParameters
    {
        StatAttribute Attribute { get; }
        int ChangePercent { get; }
    }

    public class ChangeStatsBuffParameters : BuffParameters, IChangeStatsBuffParameters
    {
        public ChangeStatsBuffParameters(
            StatAttribute attribute,
            int changePercent,
            float duration = 0,
            bool considerResist = false,
            int chance = 0,
            SkillParticles buffParticles = null)
            : base(duration, considerResist, chance, buffParticles, BuffType.ChangeStats)
        {
            Attribute = attribute;
            ChangePercent = changePercent;
        }

        public StatAttribute Attribute { get; private set; }
        public int ChangePercent { get; private set; }
    }
}
