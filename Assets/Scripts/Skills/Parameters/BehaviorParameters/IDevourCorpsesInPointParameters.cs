using Skills.Behaviors;
using Skills.Modificators;
using Skills.Particles;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IDevourCorpsesInPointParameters : IBehaviorParametersToPoint, IDevourCorpsesParameters
    {
    }

    public class DevourCorpsesInPointParameters : BehaviorParametersToPoint, IDevourCorpsesInPointParameters
    {
        public DevourCorpsesInPointParameters(
            SkillBehaviorType type,
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            SkillParticles pointParticles,
            int corpsesCount,
            int distance,
            SkillParticles collisionParticleses)
            : base(type, animationParticles, modificators, pointParticles)
        {
            CorpsesCount = corpsesCount;
            Radius = distance;
            CollisionParticleses = collisionParticleses;
        }

        public int CorpsesCount { get; private set; }
        public int Radius { get; private set; }
        public SkillParticles CollisionParticleses { get; private set; }
    }
}
