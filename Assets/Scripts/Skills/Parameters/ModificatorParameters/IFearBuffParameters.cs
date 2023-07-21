using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IFearBuffParameters : IBuffParameters
    {
    }

    public class FearBuffParameters : BuffParameters, IFearBuffParameters
    {
        public FearBuffParameters(float duration, bool considerResist, int chance, SkillParticles buffParticles) : base(
            duration,
            considerResist,
            chance,
            buffParticles,
            BuffType.Fear)
        {
        }
    }
}
