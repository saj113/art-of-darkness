using System;
using System.Collections.Generic;
using Core;
using Core.Provider;
using Core.UnityFramework;
using Skills.Behaviors;
using Skills.Behaviors.DevourCorpsesBehaviors;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Behaviors.RunRayBehaviors;
using Skills.Behaviors.SummonBehaviors;
using Skills.Behaviors.UniqueBehaviors;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnityEngine;

namespace Skills
{
    public class SkillBehaviorProvider : ISkillBehaviorProvider
    {
        private readonly ISkillCaster _caster;
        private IStats _previousAffectedTarget;
        private readonly IDictionary<IBehaviorParameters, IBehaviorActivatable> _behaviorToTargetChache;
        private readonly IGameObjectInstantiater _gameObjectInstantiater;
        private readonly ISkillColliderActivator _colliderActivator;
        private readonly ITargetUnitProvider _targetUnitProvider;

        public SkillBehaviorProvider(
            ISkillCaster caster, 
            IGameObjectInstantiater gameObjectInstantiater, 
            ISkillColliderActivator colliderActivator,
            ITargetUnitProvider targetUnitProvider)
        {
            _caster = caster;
            _targetUnitProvider = targetUnitProvider;
            _colliderActivator = colliderActivator;
            _gameObjectInstantiater = gameObjectInstantiater;
            _behaviorToTargetChache = new Dictionary<IBehaviorParameters, IBehaviorActivatable>();
        }

        public IBehaviorActivatable GetSkillBehaviorToPointComponent(
            IBehaviorParameters behaviorParameters, float targetPosition)
        {
            return GetSkillBehavior(behaviorParameters, targetPosition, null);
        }

        public IBehaviorActivatable GetSkillBehavior(IBehaviorParameters behaviorParameters, IStats target)
        {
            return GetSkillBehavior(behaviorParameters, float.NaN, target);
        }

        public IBehaviorActivatable GetSkillBehavior(IBehaviorParameters behaviorParameters)
        {
            return GetSkillBehavior(behaviorParameters, float.NaN, null);
        }
        
        private IBehaviorActivatable GetSkillBehavior(
            IBehaviorParameters behaviorParameters,
            float targetPosition,
            IStats target)
        {
            switch (behaviorParameters.Type)
            {
                case SkillBehaviorType.SummonColliderFromPoint:
                    Contract.Require(!float.IsNaN(targetPosition));
                    return new SummonColliderFromPoint(
                        _caster,
                        (ISummonColliderFromPointParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _targetUnitProvider,
                        _colliderActivator,
                        targetPosition);

                case SkillBehaviorType.RayFromPoint:
                    Contract.Require(!float.IsNaN(targetPosition));
                    return new RayFromPoint(
                        _caster,
                        (IRayFromPointParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _targetUnitProvider,
                        targetPosition);

                case SkillBehaviorType.ParticlesToCasterFromTargets:
                    Contract.Require(!float.IsNaN(targetPosition));
                    return new ParticlesToCasterFromTargets(
                        _caster,
                        (IParticlesToCasterFromTargetsParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _targetUnitProvider,
                        targetPosition);

                case SkillBehaviorType.ParticlesFromSky:
                    Contract.Require(!float.IsNaN(targetPosition));
                    return new ParticlesFromSky(
                        _caster,
                        (IParticlesParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        targetPosition);

                case SkillBehaviorType.AffectToTarget:
                case SkillBehaviorType.ParticlesToTarget:
                    Contract.Require(target != null);
                    return GetSkillBehaviortoTarget(behaviorParameters, target);

                case SkillBehaviorType.SummonSupportedColliderFromCaster:
                    return new SummonSupportedColliderFromCaster(
                        _caster,
                        (ISummonSupportedColliderFromCasterParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _colliderActivator);
                    
                default:
                    return GetGenericBehavior(behaviorParameters);
            }
        }

        private IBehaviorActivatable GetGenericBehavior(IBehaviorParameters behaviorParameters)
        {
            switch (behaviorParameters.Type)
            {
                case SkillBehaviorType.ParticlesFromCaster:
                    return new ParticlesFromCaster(
                        _caster,
                        (IParticlesFromCasterParameters)behaviorParameters,
                        _gameObjectInstantiater);

                case SkillBehaviorType.ParticlesToCaster:
                    return new ParticlesToCaster(
                        _caster,
                        (IParticlesToCasterParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _targetUnitProvider);

                case SkillBehaviorType.RayFromCaster:
                    return new RayFromCaster(
                        _caster,
                        (IRayFromCasterParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _targetUnitProvider);

                case SkillBehaviorType.SummonAcolyteFromCaster:
                    return new SummonAcolyteFromCaster(
                        _caster,
                        (ISummonAcolyteFromCasterParameters)behaviorParameters,
                        _gameObjectInstantiater);

                case SkillBehaviorType.SummonColliderFromCaster:
                    return new SummonColliderFromCaster(
                        _caster,
                        (ISummonColliderFromCasterParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _colliderActivator);

                case SkillBehaviorType.AffectToCaster:
                    return new AffectToCaster(
                        _caster,
                        (IAffectToUnitParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _targetUnitProvider);

                case SkillBehaviorType.DevourCorpses:
                    return new DevourCorpses(
                        _caster,
                        (IDevourCorpsesParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        _targetUnitProvider);

                default:
                    throw new NotSupportedException();
            }
        }

        private IBehaviorActivatable GetSkillBehaviortoTarget(IBehaviorParameters behaviorParameters, IStats target)
        {
            if (Equals(_previousAffectedTarget, target))
            {
                IBehaviorActivatable result;
                if (_behaviorToTargetChache.TryGetValue(behaviorParameters, out result))
                {
                    return result;
                }

                result = CreateSkillBehaviortoTarget(behaviorParameters, target);
                _behaviorToTargetChache.Add(behaviorParameters, result);

                return result;
            }

            _behaviorToTargetChache.Clear();
            _previousAffectedTarget = target;
            var behaviorToTarget= CreateSkillBehaviortoTarget(behaviorParameters, target);
            _behaviorToTargetChache.Add(behaviorParameters, behaviorToTarget);

            return behaviorToTarget;
        }
        private IBehaviorActivatable CreateSkillBehaviortoTarget(
            IBehaviorParameters behaviorParameters,
            IStats target)
        {
            Contract.Require(target != null);
            switch (behaviorParameters.Type)
            {
                case SkillBehaviorType.AffectToTarget:
                    return new AffectToTarget(
                        _caster,
                        (IAffectToUnitParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        target);

                case SkillBehaviorType.ParticlesToTarget:
                    return new ParticlesToTarget(
                        _caster,
                        (IParticlesFromCasterParameters)behaviorParameters,
                        _gameObjectInstantiater,
                        target);

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
