using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IRegenBuffParameters : IBuffParameters
    {
        int Interval { get; }
        int Percent { get; }
    }

    public class RegenBuffParameters : BuffParameters, IRegenBuffParameters
    {
        public RegenBuffParameters(
            float duration,
            SkillParticles buffParticles,
            int interval,
            int percent,
            BuffType type) : base(duration, false, 0, buffParticles, type)
        {
            Interval = interval;
            Percent = percent;
        }

        public int Interval { get; private set; }
        public int Percent { get; private set; }
    }
}
