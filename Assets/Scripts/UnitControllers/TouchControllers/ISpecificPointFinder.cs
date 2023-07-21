using System.Collections.Generic;
using UnityEngine;

namespace UnitControllers.TouchControllers
{
    internal interface ISpecificPointFinder
    {
        ShapeSidePoints GetShapeSidePoints(IEnumerable<Vector3> points);
        float GetLastCenterPoint();
    }
}