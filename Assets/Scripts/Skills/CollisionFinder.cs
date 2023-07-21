using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Provider;
using Stats;
using UnitControllers;
using UnityEngine;

namespace Skills
{
    public class CollisionFinder
    {
        private static readonly ITargetUnitProvider TargetUnitProvider;

        static CollisionFinder()
        {
            TargetUnitProvider = InstanceContainer.Instance.Resolve<ITargetUnitProvider>();
        }

        public static IEnumerable<IStats> FindUnitCollisions(Tag finder, TargetUnitRelation targetUnitRelation, Collider2D collider)
        {
            var units = TargetUnitProvider.Get(finder, targetUnitRelation);
            return units.Where(p => p.GameObjectController.IsCollision(collider.bounds));
        }
    }
}