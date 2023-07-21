using System;
using Stats;
using UnityEngine;

namespace Core.Trigger
{
    public interface ITriggerEvents
    {
        event Action<OnTrigger, IStats, Vector2> UnitTriggerEntered;
        event Action<OnTrigger, IStats, Vector2> UnitTriggerExited;
        event Action<OnTrigger, Tag, Vector2> ProjectileBarrierTriggered;
    }
}
