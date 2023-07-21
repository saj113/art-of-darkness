using System.Collections.Generic;
using System.Linq;
using Stats;
using UnityEngine;

namespace UnitControllers.DetectionTargets
{
    public class PlayerTargetAlgorithm : IDetectTargetAlgorithm
    {
        public IStats GetPriorityTarget(Vector2 position, IEnumerable<IStats> targets)
        {
            return targets.FirstOrDefault(p => p.Characteristics.Tag == Tag.Player);
        }
    }
}