using UnityEngine;

namespace UnitControllers.TouchControllers.ShapesRecogntions
{
    internal class CircleRecognizer : IShapeRecognizer
    {
        private const float MaxDistanceBeetwenFirstAndLastPoint = 1f;

        public ShapeType ShapeType
        {
            get { return ShapeType.Circle; }
        }

        public bool Recognize(ShapeSidePoints shapeSidePoints)
        {
            var areVerticesOrderValid = VerifyOrder(shapeSidePoints);
            var isDistanceBeetwenFirstAndLastPoint =
                Vector2.Distance(shapeSidePoints.FirstPoint, shapeSidePoints.LastPoint) <
                MaxDistanceBeetwenFirstAndLastPoint;

            return areVerticesOrderValid && isDistanceBeetwenFirstAndLastPoint;
        }


        private bool VerifyOrder(ShapeSidePoints shapeSidePoints)
        {
            if (shapeSidePoints.XMin.Order == 1)
            {
                return VerifyOrder(
                    shapeSidePoints.XMin.Order, 
                    shapeSidePoints.YMax.Order, 
                    shapeSidePoints.XMax.Order,
                    shapeSidePoints.YMin.Order);
            }

            if (shapeSidePoints.YMax.Order == 1)
            {
                return VerifyOrder(
                    shapeSidePoints.YMax.Order,
                    shapeSidePoints.XMax.Order,
                    shapeSidePoints.YMin.Order,
                    shapeSidePoints.XMin.Order);
            }

            if (shapeSidePoints.XMax.Order == 1)
            {
                return VerifyOrder(
                    shapeSidePoints.XMax.Order,
                    shapeSidePoints.YMin.Order,
                    shapeSidePoints.XMin.Order,
                    shapeSidePoints.YMax.Order);
            }

            return VerifyOrder(
                shapeSidePoints.YMin.Order,
                shapeSidePoints.XMin.Order,
                shapeSidePoints.YMax.Order,
                shapeSidePoints.XMax.Order);
        }

        private bool VerifyOrder(int point1, int point2, int point3, int point4)
        {
            return point2 == 2
                ? point3 == 3 && point4 == 4 // clockwise
                : point4 == 2 && point3 == 3 && point2 == 4; // counterclockwise
        }
    }
}
