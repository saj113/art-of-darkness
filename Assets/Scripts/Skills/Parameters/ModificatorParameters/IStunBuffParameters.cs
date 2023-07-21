using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IStunBuffParameters : IBuffParameters
    {
    }

    public class StunBuffParameters : BuffParameters, IStunBuffParameters
    {
        public StunBuffParameters(float duration, bool considerResist, int chance, SkillParticles buffParticles) 
            : base(
            duration,
            considerResist,
            chance,
            buffParticles,
            BuffType.Stun)
        {
        }
    }
}
