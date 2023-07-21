using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IBuffParameters
    {
        float Duration { get; }
        bool ConsiderResist { get; }
        int Chance { get; }
        SkillParticles BuffParticles { get; }
        BuffType Type { get; }
    }
}
