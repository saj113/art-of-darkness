using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public interface ISlowdownBuffParameters : IBuffParameters
    {
        int Power { get; }
    }

    public class SlowdownBuffParameters : BuffParameters, ISlowdownBuffParameters
    {
        public SlowdownBuffParameters(
            int power,
            float duration = 0,
            bool considerResist = false,
            int chance = 0,
            SkillParticles buffParticles = null) : base(
            duration,
            considerResist,
            chance,
            buffParticles,
            BuffType.Slowdown)
        {
            Power = power;
        }

        public int Power { get; private set; }
    }
}
