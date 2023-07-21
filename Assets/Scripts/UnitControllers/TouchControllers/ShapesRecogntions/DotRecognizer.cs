namespace UnitControllers.TouchControllers.ShapesRecogntions
{
    internal class DotRecognizer : IShapeRecognizer
    {
        private const float MaxHeightAndWidthPoint = 0.3f;

        public ShapeType ShapeType
        {
            get { return ShapeType.Dot; }
        }

        public bool Recognize(ShapeSidePoints shapeSidePoints)
        {
            var araMinimumHightValid = shapeSidePoints.YMax.Point.y - shapeSidePoints.YMin.Point.y <= MaxHeightAndWidthPoint;
            var araMinimumWidthValid = shapeSidePoints.XMax.Point.x - shapeSidePoints.XMin.Point.x <= MaxHeightAndWidthPoint;

            return araMinimumHightValid && araMinimumWidthValid;
        }
    }
}
