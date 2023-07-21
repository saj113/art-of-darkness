using System;

namespace UnitControllers.TouchControllers.ShapesRecogntions
{
    internal class VerticalLineRecognizer : IShapeRecognizer
    {
        private const float MinimumHeight = 6f;
        private const float MaximumWidth = 3f;

        public ShapeType ShapeType
        {
            get { return ShapeType.LineVertical; }
        }

        public bool Recognize(ShapeSidePoints shapeSidePoints)
        {
            var isHightValid = Math.Abs(shapeSidePoints.YMin.Point.y - shapeSidePoints.YMax.Point.y) > MinimumHeight;
            var isWidthValid = Math.Abs(shapeSidePoints.XMin.Point.x - shapeSidePoints.XMax.Point.x) < MaximumWidth;
            return isHightValid && isWidthValid;
        }
    }
}
