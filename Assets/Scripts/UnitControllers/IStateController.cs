using System;
using UnitControllers.States;

namespace UnitControllers
{
    public interface IStateController : IDisposable
    {
        StateType CurrentStateType { get; }

        void SetIdleState();
        void SetStunState();
        void SetFearState();
        void SetMoveState();
        void SetDeadState();
        void SetFlyState();
        void SetAttackState();
    }
}
