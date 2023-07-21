using System;
using Core.Provider;
using Core.UnityFramework;
using Skills;
using Stats;
using Stats.Data;
using UnityEngine;
using Utilities;

namespace Core.Trigger
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class OnTrigger : MonoBehaviour, ITriggerEvents
    {
        [SerializeField] private Tag _colliderOwnerTag = Tag.Enemy;
        [SerializeField] private TargetUnitRelation _targetUnitRelation = TargetUnitRelation.Enemy;
        public event Action<OnTrigger, IStats, Vector2> UnitTriggerEntered;
        public event Action<OnTrigger, IStats, Vector2> UnitTriggerExited;
        public event Action<OnTrigger, Tag, Vector2> ProjectileBarrierTriggered;

        private Tag ColliderOwnerTag
        {
            get { return _colliderOwnerTag; }
        }

        public void DetectWithinCollider(ITargetUnitProvider targetUnitProvider)
        {
            var bounds = GetComponent<Collider2D>().bounds;
            var statsCollection = targetUnitProvider.Get(_colliderOwnerTag,
                                                         _targetUnitRelation,
                                                         bounds,
                                                         0,
                                                         bounds.size.x / 2);
            foreach (var stats in statsCollection) { OnUnitTriggerEntered(stats); }
        }

        void OnTriggerEnter2D(Collider2D colliderComponent)
        {
            if (CanBeTriggered(colliderComponent))
            {
                var stats = GetStats(colliderComponent);
                if (stats != null) { OnUnitTriggerEntered(stats); }
            }
            else if (IsColliderProjectileBarrier(colliderComponent))
            {
                var colliderOwnerTag = colliderComponent.GetComponent<OnTrigger>().ColliderOwnerTag;
                OnProjectileBarrierTriggered(colliderOwnerTag);
            }
        }

        void OnTriggerExit2D(Collider2D colliderComponent)
        {
            if (UnitTriggerExited == null) return;

            if (CanBeTriggered(colliderComponent))
            {
                var stats = GetStats(colliderComponent);
                if (stats != null) { OnUnitTriggerExited(stats); }
            }
        }

        private bool IsColliderProjectileBarrier(Collider2D colliderComponent)
        {
            return colliderComponent.gameObject.layer == LayerMask.NameToLayer("ProjectileBarrier");
        }

        private bool CanBeTriggered(Collider2D collider)
        {
            return collider.gameObject.layer == LayerMask.NameToLayer("Unit") &&
                   ConditionUtility.CheckUnitRelationship(
                           _colliderOwnerTag,
                           collider.gameObject.tag,
                           _targetUnitRelation);
        }

        private IStats GetStats(Collider2D colliderComponent)
        {
            var characterStatsData =
                    colliderComponent.gameObject.GetComponent<CharacterStatsData>();
            if (characterStatsData != null) { return characterStatsData.Stats; }

            var unitStatsData = colliderComponent.gameObject.GetComponent<UnitStatsData>();
            return unitStatsData != null ? unitStatsData.Stats : null;
        }

        private void OnUnitTriggerEntered(IStats stats)
        {
            var handler = UnitTriggerEntered;
            if (handler != null) handler(this, stats, transform.position);
        }

        private void OnUnitTriggerExited(IStats stats)
        {
            var handler = UnitTriggerExited;
            if (handler != null) handler(this, stats, transform.position);
        }

        private void OnProjectileBarrierTriggered(Tag colliderOwnerTag)
        {
            var handler = ProjectileBarrierTriggered;
            if (handler != null) handler(this, colliderOwnerTag, transform.position);
        }
    }
}
