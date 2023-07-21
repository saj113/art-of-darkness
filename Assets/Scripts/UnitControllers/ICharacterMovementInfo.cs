using System;

namespace UnitControllers
{
    public interface ICharacterMovementInfo : IDisposable
    {
         bool IsMoving();
         int GetMoveDirection();
    }
}