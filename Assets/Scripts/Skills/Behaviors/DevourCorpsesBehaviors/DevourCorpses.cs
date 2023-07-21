using System.Linq;
using Core;
using Core.Provider;
using Core.UnityFramework;
using Skills.Parameters.BehaviorParameters;
using Stats;
using UnitControllers.States;
using UnityEngine;

namespace Skills.Behaviors.DevourCorpsesBehaviors
{
    public class DevourCorpses : SkillBehavior<IDevourCorpsesParameters>
    {
        private IStats[] _corpseCache;
        private readonly ITargetUnitProvider _targetUnitProvider;

        public DevourCorpses(
            ISkillCaster caster,
            IDevourCorpsesParameters parameters,
            IGameObjectInstantiater gameObjectInstantiater,
            ITargetUnitProvider targetUnitProvider)
            : base(caster, parameters, gameObjectInstantiater)
        {
            _targetUnitProvider = targetUnitProvider;
        }

        public override bool IsActivatable(out SkillUseFailedReason failedReason)
        {
            var result = GetCorpses().Length == Parameters.CorpsesCount;
            failedReason = result ? SkillUseFailedReason.None : SkillUseFailedReason.CorpsesNotEnoughNearby;
            return result;
        }

        protected override void ActivateCore()
        {
            foreach (var corpse in GetCorpses())
            {
                GameObjectInstantiater.TryInstantiate(Parameters.CollisionParticleses,
                    corpse.GameObjectController.Position);
                corpse.GameObjectController.Destroy();
            }

            Parameters.ApplyModificators(_targetUnitProvider.Get(Caster.Characteristics.Tag, 
                                                                 Caster.GameObjectController.Position), 
                                         !IsActivated);
        }

        public override void FinishActivation()
        {
            base.FinishActivation();
            _corpseCache = null;
        }

        protected virtual IStats[] GetCorpses(Vector2? targetPosition = null)
        {
            if (_corpseCache != null && _corpseCache.Length == Parameters.CorpsesCount)
            {
                return _corpseCache;
            }

            _corpseCache = targetPosition.HasValue
                ? _targetUnitProvider
                    .Get(
                        Caster.Characteristics.Tag,
                        TargetUnitRelation.DeadOnly,
                        targetPosition.Value.x,
                        0,
                        Parameters.Radius)
                    .Take(Parameters.CorpsesCount)
                    .ToArray()
                : _targetUnitProvider
                    .Get(
                        Caster.Characteristics.Tag,
                        TargetUnitRelation.DeadOnly,
                        Caster.GameObjectController.Bounds,
                        0,
                        Parameters.Radius)
                    .Take(Parameters.CorpsesCount)
                    .ToArray();

            return _corpseCache;
        }
    }
}