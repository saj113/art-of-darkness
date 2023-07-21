using UnityEngine;

namespace UnitControllers.TouchControllers
{
    internal struct ShapeSidePoint
    {
        private readonly int _order;
        private readonly Vector2 _point;

        public ShapeSidePoint(Vector2 point, int order)
        {
            _point = point;
            _order = order;
        }

        public int Order
        {
            get { return _order; }
        }

        public Vector2 Point
        {
            get { return _point; }
        }
    }
}
