using UnityEngine;

namespace UnitControllers.TouchControllers.ShapesRecogntions
{
    internal class ArcLeftRecognizer : IShapeRecognizer
    {
        private const int WidthToLengthRatio = 6;

        public ShapeType ShapeType
        {
            get { return ShapeType.ArcHorizontal; }
        }

        public bool Recognize(ShapeSidePoints shapeSidePoints)
        {
            var topVertice = shapeSidePoints.XMaxYMax;
            var bottomVertice = shapeSidePoints.XMaxYMin;
            var leftPoint1 = shapeSidePoints.XMinYMin.Point;
            var leftPoint2 = shapeSidePoints.XMinYMax.Point;

            var areVerticesOrderValid =
                (topVertice.Order == 1 || topVertice.Order == 4)
                && (bottomVertice.Order == 1 || bottomVertice.Order == 4);

            var areVerticesValid =
                    topVertice.Point.x > leftPoint1.x && topVertice.Point.x > leftPoint2.x
                    && topVertice.Point.y > leftPoint1.y && topVertice.Point.y > leftPoint2.y
                    && bottomVertice.Point.x > leftPoint1.x && bottomVertice.Point.x > leftPoint2.x
                    && bottomVertice.Point.y < leftPoint1.y && bottomVertice.Point.y < leftPoint2.y;

            var distanceBeetwenVertice = Mathf.Abs(topVertice.Point.y - bottomVertice.Point.y);
            var minimumWidth = distanceBeetwenVertice / WidthToLengthRatio;
            var nearestToLeftVerticePoint = topVertice.Point.x > bottomVertice.Point.x
                ? bottomVertice.Point
                : topVertice.Point;
            var araMinimumWidthValid = Mathf.Abs(shapeSidePoints.XMin.Point.x - nearestToLeftVerticePoint.x) > minimumWidth;

            return areVerticesOrderValid && areVerticesValid && araMinimumWidthValid;
        }
    }
}
