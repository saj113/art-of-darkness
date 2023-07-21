namespace Skills.Particles
{
    public interface IMovementSkillParticlesParameters
    {
        MovementSkillParticles Particles { get; }
        int ParticlesInstancesCount { get; set; }
    }
}