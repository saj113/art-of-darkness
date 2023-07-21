using Core;
using Skills.Behaviors;
using Skills.Parameters.ModificatorParameters;
using Skills.Particles;
using Stats;
using UnityEngine;

namespace Skills.Modificators.Buffs
{
    public abstract class Buff<T> : IBuff
        where T : IBuffParameters
    {
        private float _elapsedTime;
        private SkillParticles _buffParticlesInstance;


        protected Buff(ISkillCaster caster, IStats targetStatsStats, T parameters)
        {
            Caster = caster;
            TargetStats = targetStatsStats;
            Parameters = parameters;
        }

        public virtual BuffUniqueCode UniqueCode 
        {
            get { return BuffUniqueCode.NoUnique; }
        }

        protected T Parameters { get; private set; }
        protected ISkillCaster Caster { get; private set; }
        protected IStats TargetStats { get; private set; }

        

        public void Apply()
        {
            Contract.Require(Parameters.Duration > 0);

            if (Parameters.BuffParticles != null)
            {
                _buffParticlesInstance = Object.Instantiate(Parameters.BuffParticles);
                TargetStats.GameObjectController.AddChild(_buffParticlesInstance.transform);
                _buffParticlesInstance.SetPosition(TargetStats.GameObjectController.CenterPosition);
            }

            AppliedCore();
        }

        public bool IsBuffCanDelete()
        {
            _elapsedTime += Time.deltaTime;
            if (Parameters.Duration > 0 && _elapsedTime > Parameters.Duration)
            {
                return true;
            }

            return false;
        }

        public void Reset()
        {
            if (_buffParticlesInstance != null)
            {
                _buffParticlesInstance.StopEmission();
                _buffParticlesInstance = null;
            }

            ResetCore();
        }

        public virtual bool CanBeApplied() { return true; }

        protected virtual void AppliedCore()
        {

        }

        protected virtual void ResetCore()
        {

        }

        public virtual void UpdateBuff()
        {
            
        }
    }
}
