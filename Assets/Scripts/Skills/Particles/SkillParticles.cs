using System.Linq;
using Skills.Particles.State;
using UnityEngine;

namespace Skills.Particles
{
    [RequireComponent(typeof(ParticlesState))]
    public class SkillParticles : MonoBehaviour
    {
        private bool _isStopped;

        private IParticlesState[] _particlesesStates;

        public void SetPosition(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, -1);
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }

        public virtual void StopEmission()
        {
            foreach(var ps in _particlesesStates)
            {
                ps.StopEmission();
            }

            _isStopped = true;
        }

        public void Resize(float scaleX)
        {
            transform.localScale = new Vector2(scaleX, transform.localScale.y);
        }

        protected virtual void VirtualUpdate()
        {
            
        }

        protected virtual void VirtualStart()
        {
            
        }

        void Awake()
        {
            _particlesesStates = GetComponentsInChildren<ParticlesState>();
            VirtualStart();
        }

        void FixedUpdate()
        {
            if (_particlesesStates.All(p => p.IsFinished))
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (!_isStopped)
            {
                VirtualUpdate();
            }
        }
    }
}
