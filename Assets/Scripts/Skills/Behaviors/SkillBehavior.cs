using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;

namespace Skills.Behaviors
{
    public abstract class SkillBehavior<T> : IBehaviorActivatable
        where T : IBehaviorParameters
    {
        private AnimationSkillParticles _animationParticles;

        protected T Parameters { get; private set; }
        protected ISkillCaster Caster { get; private set; }
        protected bool IsActivated { get; private set; }
        protected IGameObjectInstantiater GameObjectInstantiater { get; private set; }

        protected SkillBehavior(ISkillCaster caster, T parameters, IGameObjectInstantiater gameObjectInstantiater)
        {
            GameObjectInstantiater = gameObjectInstantiater;
            Caster = caster;
            Parameters = parameters;
        }

        public void Activate()
        {
            if (Parameters.AnimationParticles != null && _animationParticles == null)
            {
                _animationParticles = GameObjectInstantiater.Instantiate(Parameters.AnimationParticles);
                _animationParticles.Initialize(Caster);
            }

            ActivateCore();

            IsActivated = true;
        }

        public virtual void FinishActivation()
        {
            if (_animationParticles != null)
            {
                _animationParticles.StopEmission();
            }

            IsActivated = false;
        }

        public virtual bool IsActivatable(out SkillUseFailedReason failedReason)
        {
            failedReason = SkillUseFailedReason.None;
            return true;
        }

        protected abstract void ActivateCore();
    }
}
