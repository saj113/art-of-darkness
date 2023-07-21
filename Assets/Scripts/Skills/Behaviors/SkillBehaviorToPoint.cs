using System.Collections.Generic;
using System.Linq;
using Core.Provider;
using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Skills.Particles;
using Stats;
using UnityEngine;
using Utilities;

namespace Skills.Behaviors
{
    public abstract class SkillBehaviorToPoint<T> : SkillBehavior<T>
        where T : IBehaviorParametersToPoint
    {
        private IStats[] _unitsCache;
        private SkillParticles _pointParticlesInstance;
        private readonly ITargetUnitProvider _targetUnitProvider;

        protected SkillBehaviorToPoint(
                ISkillCaster caster,
                T parameters,
                IGameObjectInstantiater gameObjectInstantiater,
                ITargetUnitProvider targetUnitProvider,
                float targetPosition)
                : base(caster, parameters, gameObjectInstantiater)
        {
            _targetUnitProvider = targetUnitProvider;
            TargetPosition = targetPosition;
        }

        protected float TargetPosition { get; private set; }

        protected override void ActivateCore()
        {
            if (Parameters.PointParticles == null || _pointParticlesInstance != null) return;
            _pointParticlesInstance = GameObjectInstantiater.Instantiate(Parameters.PointParticles);
            _pointParticlesInstance.SetPosition(new Vector2(TargetPosition, 0));
        }

        public override void FinishActivation()
        {
            if (_pointParticlesInstance != null)
            {
                _pointParticlesInstance.StopEmission();
            }

            base.FinishActivation();
        }

        protected virtual IStats[] GetUnits(
            float targetPosition,
            TargetUnitRelation targetUnitRelation = TargetUnitRelation.Enemy,
            float distance = 30,
            int maxTargetCount = 10)
        {
            if (_unitsCache != null && _unitsCache.Any())
            {
                return _unitsCache;
            }

            _unitsCache = _targetUnitProvider.Get(Caster.Characteristics.Tag,
                                                  targetUnitRelation,
                                                  targetPosition,
                                                  0,
                                                  distance)
                                             .Take(maxTargetCount)
                                             .ToArray();

            return _unitsCache;
        }
    }
}
