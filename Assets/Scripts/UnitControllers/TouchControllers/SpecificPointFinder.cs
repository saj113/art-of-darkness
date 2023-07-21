using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnitControllers.TouchControllers
{
    internal class SpecificPointFinder : ISpecificPointFinder
    {
        private Vector3 _centerPoint;
        public ShapeSidePoints GetShapeSidePoints(IEnumerable<Vector3> points)
        {
            _centerPoint = GetCenterPoint(points);
            var firstPoint = points.First();
            var lastPoint = points.Last();
            var xMinYminPoint = _centerPoint;
            var xMaxYminPoint = _centerPoint;
            var xMinYmaxPoint = _centerPoint;
            var xMaxYmaxPoint = _centerPoint;
            var xMinPoint = _centerPoint;
            var xMaxPoint = _centerPoint;
            var yMinPoint = _centerPoint;
            var yMaxPoint = _centerPoint;
            var xMinYminIndex = 0;
            var xMaxYminIndex = 0;
            var xMinYmaxIndex = 0;
            var xMaxYmaxIndex = 0;
            var xMinIndex = 0;
            var xMaxIndex = 0;
            var yMaxIndex = 0;
            var yMinIndex = 0;
            var i = 0;
            foreach (var point in points)
            {
                if (!xMinYminPoint.Equals(point) && xMinYminPoint.x >= point.x && xMinYminPoint.y >= point.y)
                {
                    xMinYminPoint = point;
                    xMinYminIndex = i;
                }

                if (!xMaxYminPoint.Equals(point) && xMaxYminPoint.x <= point.x && xMaxYminPoint.y >= point.y)
                {
                    xMaxYminPoint = point;
                    xMaxYminIndex = i;
                }

                if (!xMinYmaxPoint.Equals(point) && xMinYmaxPoint.x >= point.x && xMinYmaxPoint.y <= point.y)
                {
                    xMinYmaxPoint = point;
                    xMinYmaxIndex = i;
                }

                if (!xMaxYmaxPoint.Equals(point) && xMaxYmaxPoint.x <= point.x && xMaxYmaxPoint.y <= point.y)
                {
                    xMaxYmaxPoint = point;
                    xMaxYmaxIndex = i;
                }

                if (point.x > xMaxPoint.x)
                {
                    xMaxPoint = point;
                    xMaxIndex = i;
                }

                if (point.x < xMinPoint.x)
                {
                    xMinPoint = point;
                    xMinIndex = i;
                }

                if (point.y > yMaxPoint.y)
                {
                    yMaxPoint = point;
                    yMaxIndex = i;
                }

                if (point.y < yMinPoint.y)
                {
                    yMinPoint = point;
                    yMinIndex = i;
                }

                i++;
            }

            var shapeSidePoints = new ShapeSidePoints(
                new ShapeSidePoint(
                    xMinYminPoint, GetOrderByIndexes(xMinYminIndex, xMaxYminIndex, xMinYmaxIndex, xMaxYmaxIndex)),
                new ShapeSidePoint(
                    xMaxYminPoint, GetOrderByIndexes(xMaxYminIndex, xMinYminIndex, xMinYmaxIndex, xMaxYmaxIndex)),
                new ShapeSidePoint(
                    xMinYmaxPoint, GetOrderByIndexes(xMinYmaxIndex, xMinYminIndex, xMaxYminIndex, xMaxYmaxIndex)),
                new ShapeSidePoint(
                    xMaxYmaxPoint, GetOrderByIndexes(xMaxYmaxIndex, xMinYminIndex, xMaxYminIndex, xMinYmaxIndex)),
                new ShapeSidePoint(
                    xMinPoint, GetOrderByIndexes(xMinIndex, xMaxIndex, yMinIndex, yMaxIndex)),
                new ShapeSidePoint(
                    xMaxPoint, GetOrderByIndexes(xMaxIndex, xMinIndex, yMinIndex, yMaxIndex)),
                new ShapeSidePoint(
                    yMinPoint, GetOrderByIndexes(yMinIndex, xMaxIndex, xMinIndex, yMaxIndex)),
                new ShapeSidePoint(
                    yMaxPoint, GetOrderByIndexes(yMaxIndex, xMaxIndex, yMinIndex, xMinIndex)),
                firstPoint, lastPoint);
            return shapeSidePoints;
        }

        public float GetLastCenterPoint()
        {
            return _centerPoint.x;
        }

        private Vector3 GetCenterPoint(IEnumerable<Vector3> points)
        {
            var pointsCount = points.Count();
            var totalX = 0.0f;
            var totalY = 0.0f;
            foreach (var p in points)
            {
                totalX += p.x;
                totalY += p.y;
            }

            return new Vector2(totalX / pointsCount, totalY / pointsCount);
        }

        private int GetOrderByIndexes(int currentIndex, int point1Index, int point2Index, int point3Index)
        {
            var order = 1;
            if (currentIndex > point1Index)
            {
                order++;
            }
            if (currentIndex > point2Index)
            {
                order++;
            }
            if (currentIndex > point3Index)
            {
                order++;
            }

            return order;
        }
    }
}
