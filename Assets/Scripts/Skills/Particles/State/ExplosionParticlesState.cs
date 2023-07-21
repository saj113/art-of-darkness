namespace Skills.Particles.State
{
    public class ExplosionParticlesState : ParticlesState
    {
        private bool _isStarted;

        protected override bool IsFinishedCore()
        {
            if (_isStarted) return PS.particleCount == 0;

            _isStarted = PS.particleCount > 0;
            return false;
        }
    }
}
