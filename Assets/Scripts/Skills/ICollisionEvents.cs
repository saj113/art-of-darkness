using System;
using Stats;
using UnityEngine;

namespace Skills
{
    public interface ICollisionEvents
    {
        event Action<IStats, Vector2> UnitCollisionEntered;
        event Action<IStats, Vector2> UnitCollisionExited;
        event Action<Tag, Vector2> ProjectileBarrierTriggered;

        void Update();
    }
}