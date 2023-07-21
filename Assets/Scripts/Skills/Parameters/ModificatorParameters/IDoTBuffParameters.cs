using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IDoTBuffParameters : IBuffParameters
    {
        int Interval { get; }
        int Power { get; }
    }

    public class DoTBuffParameters : BuffParameters, IDoTBuffParameters
    {
        public DoTBuffParameters(
            float duration,
            bool considerResist,
            int chance,
            SkillParticles buffParticles,
            int interval,
            int power) : base(
            duration,
            considerResist,
            chance,
            buffParticles,
            BuffType.DoT)
        {
            Interval = interval;
            Power = power;
        }

        public int Interval { get; private set; }
        public int Power { get; private set; }
    }
}
