using Skills.Behaviors;
using Skills.Particles;
using UnitControllers.AcolytesBehavior;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IEnslaveBuffParameters : IBuffParameters
    {
        AcolyteType AcolyteType { get; }
    }

    public class EnslaveBuffParameters : BuffParameters, IEnslaveBuffParameters
    {
        public EnslaveBuffParameters(
            AcolyteType acolyteType, 
            float duration = 0, 
            bool considerResist = false, 
            int chance = 0, 
            SkillParticles buffParticles = null) :
            base(duration, considerResist, chance, buffParticles, BuffType.Enslave)
        {
            AcolyteType = acolyteType;
        }

        public AcolyteType AcolyteType { get; private set; }
    }
}
