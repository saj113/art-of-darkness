using UnityEngine;

namespace UnitControllers.TouchControllers
{
    internal struct ShapeSidePoints
    {
        private readonly ShapeSidePoint _xMinYmin;
        private readonly ShapeSidePoint _xMaxYmin;
        private readonly ShapeSidePoint _xMinYmax;
        private readonly ShapeSidePoint _xMaxYmax;
        private readonly ShapeSidePoint _xMin;
        private readonly ShapeSidePoint _xMax;
        private readonly ShapeSidePoint _yMin;
        private readonly ShapeSidePoint _yMax;
        private readonly Vector2 _firstPoint;
        private readonly Vector2 _lastPoint;

        public ShapeSidePoints(
            ShapeSidePoint xMinYmin, ShapeSidePoint xMaxYmin, ShapeSidePoint xMinYmax, ShapeSidePoint xMaxYmax,
            ShapeSidePoint xMin, ShapeSidePoint xMax, ShapeSidePoint yMin, ShapeSidePoint yMax, Vector2 firstPoint, Vector2 lastPoint)
        {
            _xMinYmin = xMinYmin;
            _xMaxYmin = xMaxYmin;
            _xMinYmax = xMinYmax;
            _xMaxYmax = xMaxYmax;
            _xMin = xMin;
            _xMax = xMax;
            _yMin = yMin;
            _yMax = yMax;
            _firstPoint = firstPoint;
            _lastPoint = lastPoint;
        }

        public ShapeSidePoint XMinYMin
        {
            get { return _xMinYmin; }
        }

        public ShapeSidePoint XMaxYMin
        {
            get { return _xMaxYmin; }
        }

        public ShapeSidePoint XMinYMax
        {
            get { return _xMinYmax; }
        }

        public ShapeSidePoint XMaxYMax
        {
            get { return _xMaxYmax; }
        }

        public ShapeSidePoint XMin
        {
            get { return _xMin; }
        }

        public ShapeSidePoint XMax
        {
            get { return _xMax; }
        }

        public ShapeSidePoint YMin
        {
            get { return _yMin; }
        }

        public ShapeSidePoint YMax
        {
            get { return _yMax; }
        }

        public Vector2 FirstPoint
        {
            get { return _firstPoint; }
        }

        public Vector2 LastPoint
        {
            get { return _lastPoint; }
        }
    }
}
