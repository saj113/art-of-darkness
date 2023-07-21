using UnityEngine;

namespace UnitControllers.TouchControllers.ShapesRecogntions
{
    internal class ArcUpRecognizer : IShapeRecognizer
    {
        private const int HeightToLengthRatio = 4;
        private const int MinDistanceBeetwenVertices = 2;

        public ShapeType ShapeType
        {
            get { return ShapeType.ArcUp; }
        }

        public bool Recognize(ShapeSidePoints shapeSidePoints)
        {
            var leftVertice = shapeSidePoints.XMinYMin;
            var rightVertice = shapeSidePoints.XMaxYMin;
            var bottomPoint1 = shapeSidePoints.XMinYMax.Point;
            var bottomPoint2 = shapeSidePoints.XMaxYMax.Point;

            var areVerticesOrderValid =
                (leftVertice.Order == 1 || leftVertice.Order == 4)
                && (rightVertice.Order == 1 || rightVertice.Order == 4);

            var areVerticesValid =
                    leftVertice.Point.x < bottomPoint1.x && leftVertice.Point.x < bottomPoint2.x
                    && leftVertice.Point.y < bottomPoint1.y && leftVertice.Point.y < bottomPoint2.y
                    && rightVertice.Point.x > bottomPoint1.x && rightVertice.Point.x > bottomPoint2.x
                    && rightVertice.Point.y < bottomPoint1.y && rightVertice.Point.y < bottomPoint2.y;

            var distanceBeetwenVertice = Mathf.Abs(leftVertice.Point.x - rightVertice.Point.x);
            var isDistanceBeetwenVerticeValid = distanceBeetwenVertice > MinDistanceBeetwenVertices;
            var minimumHight = distanceBeetwenVertice / HeightToLengthRatio;
            var higherVerticePoint = leftVertice.Point.y < rightVertice.Point.y
                ? rightVertice.Point
                : leftVertice.Point;
            var araMinimumHightValid = Mathf.Abs(shapeSidePoints.YMax.Point.y - higherVerticePoint.y) > minimumHight;

            return areVerticesOrderValid && isDistanceBeetwenVerticeValid && areVerticesValid && araMinimumHightValid;
        }
    }
}
