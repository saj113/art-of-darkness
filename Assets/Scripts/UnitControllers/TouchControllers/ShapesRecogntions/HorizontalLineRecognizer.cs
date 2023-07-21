using System;

namespace UnitControllers.TouchControllers.ShapesRecogntions
{
    internal class HorizontalLineRecognizer : IShapeRecognizer
    {
        private const float MaximumHeight = 3f;
        private const float MinimumWidth = 6f;

        public ShapeType ShapeType
        {
            get { return ShapeType.LineHorizontal; }
        }

        public bool Recognize(ShapeSidePoints shapeSidePoints)
        {
            var isHightValid = Math.Abs(shapeSidePoints.YMin.Point.y - shapeSidePoints.YMax.Point.y) < MaximumHeight;
            var isWidthValid = Math.Abs(shapeSidePoints.XMin.Point.x - shapeSidePoints.XMax.Point.x) > MinimumWidth;

            return isHightValid && isWidthValid;
        }
    }
}
