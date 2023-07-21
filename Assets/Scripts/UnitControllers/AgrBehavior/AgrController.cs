using System;
using System.Collections.Generic;

namespace UnitControllers.AgrBehavior
{
    public class AgrController : IAgrController
    {
        private readonly IDictionary<Guid, int> _affectedTarget = new Dictionary<Guid, int>();
        public void AddDamagedTarget(Guid targetId, int amount)
        {
            amount = amount < 0 ? amount * -1 : amount;

            int oldValue;
            if (_affectedTarget.TryGetValue(targetId, out oldValue))
            {
                _affectedTarget[targetId] = oldValue + amount;
            }
            else
            {
                _affectedTarget[targetId] = amount;
            }
        }

        public int GetDamageAmountToTarget(Guid targetId)
        {
            int value;
            if (_affectedTarget.TryGetValue(targetId, out value))
            {
                return value;
            }

            return 0;
        }
    }
}