namespace Skills.Particles.State
{
    public class TrailParticlesState : ParticlesState
    {
        protected override bool IsFinishedCore() { return IsStopped && PS.particleCount == 0; }
    }
}
