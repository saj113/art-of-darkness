using UnityEngine;

namespace UnitControllers.TouchControllers.ShapesRecogntions
{
    internal class ArcRightRecognizer : IShapeRecognizer
    {
        private const int WidthToLengthRatio = 6;

        public ShapeType ShapeType
        {
            get { return ShapeType.ArcHorizontal; }
        }

        public bool Recognize(ShapeSidePoints shapeSidePoints)
        {
            var topVertice = shapeSidePoints.XMinYMax;
            var bottomVertice = shapeSidePoints.XMinYMin;
            var rightPoint1 = shapeSidePoints.XMaxYMin;
            var rightPoint2 = shapeSidePoints.XMaxYMax;

            var areVerticesOrderValid =
                (topVertice.Order == 1 || topVertice.Order == 4)
                && (bottomVertice.Order == 1 || bottomVertice.Order == 4);

            var areVerticesValid =
                    topVertice.Point.x < rightPoint1.Point.x && topVertice.Point.x < rightPoint2.Point.x
                    && topVertice.Point.y > rightPoint1.Point.y && topVertice.Point.y > rightPoint2.Point.y
                    && bottomVertice.Point.x < rightPoint1.Point.x && bottomVertice.Point.x < rightPoint2.Point.x
                    && bottomVertice.Point.y < rightPoint1.Point.y && bottomVertice.Point.y < rightPoint2.Point.y;

            var distanceBeetwenVertice = Mathf.Abs(topVertice.Point.y - bottomVertice.Point.y);
            var minimumWidth = distanceBeetwenVertice / WidthToLengthRatio;
            var nearestToRightVerticePoint = topVertice.Point.x < bottomVertice.Point.x
                ? bottomVertice.Point
                : topVertice.Point;
            var araMinimumWidthValid = Mathf.Abs(shapeSidePoints.XMax.Point.x - nearestToRightVerticePoint.x) > minimumWidth;

            return areVerticesOrderValid && areVerticesValid && araMinimumWidthValid;
        }
    }
}
