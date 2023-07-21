using UnityEngine;

namespace Skills.Particles.State
{
    public class OverTimeParticlesState : ParticlesState
    {
        protected override bool IsFinishedCore()
        {
            return IsStopped && Mathf.Abs(PS.emission.rateOverTime.constant) < 0.1f;
        }
    }
}
