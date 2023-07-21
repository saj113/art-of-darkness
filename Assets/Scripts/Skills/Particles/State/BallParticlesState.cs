namespace Skills.Particles.State
{
    public class BallParticlesState : ParticlesState
    {
        protected override bool IsFinishedCore() { return IsStopped; }
    }
}
