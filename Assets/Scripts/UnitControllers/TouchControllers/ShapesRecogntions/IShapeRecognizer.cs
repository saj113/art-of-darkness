namespace UnitControllers.TouchControllers.ShapesRecogntions
{
    internal interface IShapeRecognizer
    {
        ShapeType ShapeType { get; }
        bool Recognize(ShapeSidePoints shapeSidePoints);
    }
}
