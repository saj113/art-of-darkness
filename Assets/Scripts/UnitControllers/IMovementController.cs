namespace UnitControllers
{
    public interface IMovementController
    {
        void MoveWithVelocity(int direction);
        void Stop();
        void MoveToTargetWithDelay(float targetXPosition);
    }
}
