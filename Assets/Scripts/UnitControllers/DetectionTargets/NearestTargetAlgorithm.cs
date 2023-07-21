using System.Collections.Generic;
using System.Linq;
using Stats;
using UnityEngine;

namespace UnitControllers.DetectionTargets
{
    internal class NearestTargetAlgorithm : IDetectTargetAlgorithm
    {
        public IStats GetPriorityTarget(Vector2 position, IEnumerable<IStats> targets)
        {
            if (!targets.Any())
            {
                return null;
            }

            var nearestTarget = targets.First();
            var minDistance = Vector2.Distance(position, targets.First().GameObjectController.CenterPosition);
            foreach (var target in targets.Skip(1))
            {
                var distance = Vector2.Distance(position, target.GameObjectController.CenterPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = target;
                }
            }

            return nearestTarget;
        }
    }
}
