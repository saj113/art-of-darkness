using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace UnitControllers.DetectionTargets
{
    public interface IDetectTargetAlgorithm
    {
        IStats GetPriorityTarget(Vector2 position, IEnumerable<IStats> targets);
    }
}
