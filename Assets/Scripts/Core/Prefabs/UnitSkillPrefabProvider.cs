using Skills.Behaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Particles;

namespace Core.Prefabs
{
    public class UnitSkillPrefabProvider : PrefabProvider, IUnitSkillPrefabProvider
    {
        private readonly MovementSkillParticles _fireball1;
        private readonly SkillParticles _fireball1Collision;
        private readonly MovementSkillParticles _fireball2;
        private readonly SkillParticles _fireball2Collision;

        public UnitSkillPrefabProvider()
        {
            _fireball1 = GetPrefab<MovementSkillParticles>("Particles/Units/Fireball1");
            _fireball1Collision = GetPrefab<SkillParticles>("Particles/Units/Fireball1Collision");
            _fireball2 = GetPrefab<MovementSkillParticles>("Particles/Units/Fireball2");
            _fireball2Collision = GetPrefab<SkillParticles>("Particles/Units/Fireball2Collision");
        }

        public MovementSkillParticles GetFireball1() { return _fireball1; }
        public SkillParticles GetFireball1Collision() { return _fireball1Collision; }
        
        public MovementSkillParticles GetFireball2() { return _fireball2; }
        public SkillParticles GetFireball2Collision() { return _fireball2Collision; }
    }
}