using System.Collections.Generic;
using Core;
using Core.Trigger;
using Core.UnityFramework;
using Skills.Modificators.Buffs;
using Skills.Parameters;
using Stats;
using UnitControllers;
using UnityEngine;
using Utilities;

namespace Skills.Behaviors.SummonBehaviors.ColliderComponents
{
    public class AffectWhileInAreaCollider : TemporaryBehaviorCollider
    {
        private readonly IDictionary<IBuffController, IList<IBuff>> _buffsByTargets = 
            new Dictionary<IBuffController, IList<IBuff>>();

        public AffectWhileInAreaCollider(
            IGameObject collider, ISkillCaster caster,
            IColliderParameters parameters, ICollisionEvents collisionEvents, 
            IGameObjectInstantiater instantiater, IUnityUpdateEvents updateEvents)
            : base(collider, caster, parameters, collisionEvents, instantiater, updateEvents)
        {
        }

        protected override void CollisionDetected(IStats target, Vector2 pos)
        {
            if (!ConditionUtility.CheckUnitRelationship(Caster, target, Parameters.TargetUnitRelation))
            {
                return;
            }

            if (!_buffsByTargets.ContainsKey(target.BuffController))
            {
                target.BuffController.BuffAdded += BuffControllerOnBuffAdded;

                Parameters.Modificators.ApplyAll(target);
                InstantiateParticles(Parameters.CollisionParticles);

                target.BuffController.BuffAdded -= BuffControllerOnBuffAdded;
            }
        }

        public override void Dispose()
        {
            try
            {
                foreach (var targetBuffController in _buffsByTargets.Keys)
                {
                    foreach (var buff in _buffsByTargets[targetBuffController])
                    {
                        targetBuffController.RemoveBuff(buff);
                    }
                }

                _buffsByTargets.Clear();
            }
            finally
            {
                base.Dispose();
            }
        }

        protected override void OnCollisionExited(IStats target, Vector2 pos)
        {
            IList<IBuff> targetBuffList;
            if (_buffsByTargets.TryGetValue(target.BuffController, out targetBuffList))
            {
                foreach (var buff in targetBuffList)
                {
                    target.BuffController.RemoveBuff(buff);
                }

                _buffsByTargets.Remove(target.BuffController);
            }
        }

        private void BuffControllerOnBuffAdded(IBuffController buffController, IBuff buff)
        {
            IList<IBuff> targetBuffList;
            if (_buffsByTargets.TryGetValue(buffController, out targetBuffList))
            {
                targetBuffList.Add(buff);
            }
            else
            {
                _buffsByTargets.Add(buffController, new List<IBuff> { buff });
            }
        }
    }
}
