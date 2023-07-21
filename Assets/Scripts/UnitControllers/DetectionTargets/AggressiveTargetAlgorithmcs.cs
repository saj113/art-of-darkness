using System;
using System.Collections.Generic;
using System.Linq;
using Stats;
using UnityEngine;

namespace UnitControllers.DetectionTargets
{
    internal class AggressiveTargetAlgorithmcs : IDetectTargetAlgorithm
    {
        private readonly IDetectTargetAlgorithm _innerAlgorithm;
        private readonly Guid _unitId;
        public AggressiveTargetAlgorithmcs(Guid unitId, IDetectTargetAlgorithm innerAlgorithm)
        {
            _unitId = unitId;
            _innerAlgorithm = innerAlgorithm;
        }

        public IStats GetPriorityTarget(Vector2 position, IEnumerable<IStats> targets)
        {
            var mostAggressiveTarget = targets
                .OrderByDescending(p => p.AgrController.GetDamageAmountToTarget(_unitId))
                .FirstOrDefault();
            if (mostAggressiveTarget != null && mostAggressiveTarget.AgrController.GetDamageAmountToTarget(_unitId) > 0) 
            {
                return mostAggressiveTarget;
            }

            return _innerAlgorithm.GetPriorityTarget(position, targets);
        }
    }
}