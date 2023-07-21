using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IFlyBuffParameters : IBuffParameters
    {
        float Speed { get; }
    }

    public class FlyBuffParameters : BuffParameters, IFlyBuffParameters
    {
        public FlyBuffParameters(float duration, bool considerResist, int chance, SkillParticles buffParticles, float speed) : base(
            duration,
            considerResist,
            chance,
            buffParticles,
            BuffType.Fly)
        {
            Speed = speed;
        }

        public float Speed { get; private set; }
    }
}