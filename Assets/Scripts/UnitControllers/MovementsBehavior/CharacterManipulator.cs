using UnityEngine;

namespace UnitControllers.MovementsBehavior
{
    public class CharacterMovementInfo : ICharacterMovementInfo
    {
        public int GetMoveDirection()
        {
            return (int)Input.GetAxisRaw("Horizontal");
        }

        public bool IsMoving()
        {
            return GetMoveDirection() != 0;
        }

        public void Dispose()
        {
        }
    }
}