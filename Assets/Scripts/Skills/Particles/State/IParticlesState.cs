namespace Skills.Particles.State
{
    public interface IParticlesState
    {
        bool IsFinished {get;}
        void StopEmission();
    }
}