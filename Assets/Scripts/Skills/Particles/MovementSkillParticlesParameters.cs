namespace Skills.Particles
{
    public class MovementSkillParticlesParameters : IMovementSkillParticlesParameters
    {public MovementSkillParticlesParameters(int particlesInstancesCount, MovementSkillParticles particles)
        {
            ParticlesInstancesCount = particlesInstancesCount;
            Particles = particles;
        }

        public int ParticlesInstancesCount { get; set; }

        public MovementSkillParticles Particles { get; private set; }
    }
}
