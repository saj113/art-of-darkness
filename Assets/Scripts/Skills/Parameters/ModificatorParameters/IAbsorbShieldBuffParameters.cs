using Skills.Behaviors;
using Skills.Particles;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IAbsorbShieldBuffParameters : IBuffParameters
    {
        int AbsorbingHealthPercent { get; }
    }

    public class AbsorbShieldBuffParameters : BuffParameters, IAbsorbShieldBuffParameters
    {
        public AbsorbShieldBuffParameters(
            float duration,
            bool considerResist,
            int chance,
            SkillParticles buffParticles,
            int absorbingHealthPercent)
            : base(duration, considerResist, chance, buffParticles, BuffType.AbsorbShield)
        {
            AbsorbingHealthPercent = absorbingHealthPercent;
        }

        public int AbsorbingHealthPercent { get; private set; }
    }
}
