using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Particles;

namespace Core.Prefabs
{
    public interface IUnitSkillPrefabProvider
    {
        MovementSkillParticles GetFireball1();
        SkillParticles GetFireball1Collision();
        MovementSkillParticles GetFireball2();
        SkillParticles GetFireball2Collision();
        
    }
}
