using Skills.Behaviors;
using Skills.Modificators;
using Skills.Particles;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IDevourCorpsesParameters : IBehaviorParameters
    {
        int CorpsesCount { get; }
        int Radius { get; }
        SkillParticles CollisionParticleses { get; }
    }

    public class DevourCorpsesParameters : BehaviorParameters, IDevourCorpsesParameters
    {
        public DevourCorpsesParameters(
            AnimationSkillParticles animationParticles,
            IModificator[] modificators,
            int corpsesCount,
            int radius,
            SkillParticles collisionParticleses)
            : base(SkillBehaviorType.DevourCorpses, animationParticles, modificators)
        {
            CorpsesCount = corpsesCount;
            Radius = radius;
            CollisionParticleses = collisionParticleses;
        }

        public int CorpsesCount { get; private set; }
        public int Radius { get; private set; }
        public SkillParticles CollisionParticleses { get; private set; }
    }
}
