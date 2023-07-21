using System;

namespace UnitControllers
{
    public interface IAgrController
    {
        void AddDamagedTarget(Guid targetId, int amount);

        int GetDamageAmountToTarget(Guid targetId);
    }
}