using System;
using System.Collections.Generic;
using System.Linq;
using Stats;
using UnityEngine;

namespace Skills
{
    public sealed class CollisionEvents : ICollisionEvents
    {
        private readonly Tag _owner;
        private readonly TargetUnitRelation _targetUnitRelation;
        private readonly Collider2D _collider;
        private readonly HashSet<IStats> _detectedCollisions = new HashSet<IStats>();
        private readonly HashSet<IStats> _collisionsForEvent = new HashSet<IStats>();

        public CollisionEvents(Tag owner, TargetUnitRelation targetUnitRelation, Collider2D collider)
        {
            _owner = owner;
            _targetUnitRelation = targetUnitRelation;
            _collider = collider;
        }
        
        public event Action<IStats, Vector2> UnitCollisionEntered;
        public event Action<IStats, Vector2> UnitCollisionExited;
        public event Action<Tag, Vector2> ProjectileBarrierTriggered;

        public void Update()
        {
            var collisions = CollisionFinder.FindUnitCollisions(_owner, _targetUnitRelation, _collider);
            foreach (var collision in collisions)
            {
                if (!_detectedCollisions.Contains(collision))
                {
                    _detectedCollisions.Add(collision);
                    _collisionsForEvent.Add(collision);
                }
            }

            foreach (var collision in _collisionsForEvent)
            {
                OnUnitCollisionEntered(collision);
            }
            
            _collisionsForEvent.Clear();

            foreach (var detectedCollision in _detectedCollisions)
            {
                if (!collisions.Contains(detectedCollision))
                {
                    _collisionsForEvent.Add(detectedCollision);
                }
            }
            
            foreach (var collision in _collisionsForEvent)
            {
                OnUnitCollisionExited(collision);
            }
            
            _collisionsForEvent.Clear();
            _detectedCollisions.RemoveWhere(p => !collisions.Contains(p));
        }

        private void OnUnitCollisionEntered(IStats unit)
        {
            UnitCollisionEntered?.Invoke(unit, _collider.bounds.center);
        }

        private void OnUnitCollisionExited(IStats unit)
        {
            UnitCollisionExited?.Invoke(unit, _collider.bounds.center);
        }

        private void OnProjectileBarrierTriggered(Tag unit)
        {
            ProjectileBarrierTriggered?.Invoke(unit, _collider.bounds.center);
        }
    }
}