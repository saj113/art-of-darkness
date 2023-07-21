using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public abstract class BuffParameters : IBuffParameters
    {
        protected BuffParameters(
            float duration,
            bool considerResist,
            int chance,
            SkillParticles buffParticles,
            BuffType type)
        {
            Duration = duration;
            ConsiderResist = considerResist;
            Chance = chance;
            BuffParticles = buffParticles;
            Type = type;
        }

        public float Duration { get; private set; }
        public bool ConsiderResist { get; private set; }
        public int Chance { get; private set; }
        public SkillParticles BuffParticles { get; private set; }
        public BuffType Type { get; private set; }
    }
}
