using UnityEngine;

namespace Skills.Particles.State
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class ParticlesState : MonoBehaviour, IParticlesState
    {
        private ParticleSystemRenderer _render;

        public bool IsFinished { get; private set; }
        
        protected ParticleSystem PS { get; private set; }

        protected bool IsStopped  { get; private set; }

        public void StopEmission()
        {
            PS.Stop();
            IsStopped = true;
        }

        protected abstract bool IsFinishedCore();

        void Awake()
        {
            _render = GetComponent<ParticleSystemRenderer>();
            PS = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if (IsFinished) return;
            if (!IsFinishedCore()) return;

            _render.enabled = false;
            IsFinished = true;
        }
    }
}