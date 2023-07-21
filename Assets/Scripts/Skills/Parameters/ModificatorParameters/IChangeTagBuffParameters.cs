using Skills.Behaviors;
using Skills.Particles;
using Stats;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IChangeTagBuffParameters : IBuffParameters
    {
        Tag Tag { get; }
    }

    public class ChangeTagBuffParameters : BuffParameters, IChangeTagBuffParameters
    {
        public ChangeTagBuffParameters(
            float duration,
            bool considerResist,
            int chance,
            SkillParticles buffParticles,
            Tag tag) : base(duration, considerResist, chance, buffParticles, BuffType.ChangeTag)
        {
            Tag = tag;
        }

        public Tag Tag { get; private set; }
    }
}
