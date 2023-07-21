using System.Linq;
using Core;
using Core.Animation;
using Skills.Parameters;
using Skills.Weapons;
using Stats;
using UnitControllers.AcolytesBehavior;
using UnitControllers.DetectionTargets;
using UnityEngine;
using Utilities;

namespace UnitControllers.States
{
    internal class UnitIdleState : State
    {
        private readonly IUnitStateController _stateController;
        private readonly IPriorityTargetProvider _priorityTargetProvider;
        private readonly IFollowingController _followingController;
        private readonly IStateAnimationController _animationController;
        private readonly IUnitWeapon _unitWeapon;
        private readonly IMovementController _movementController;
        private ISkillParameters _skillForUse;
        private readonly int _randomRangeDeviationPercent;
        private readonly IUnitGameObjectController _unitGameObjectController;

        public UnitIdleState(
            IUnitStateController stateController, 
            IPriorityTargetProvider priorityTargetProvider,
            IFollowingController followingController,
            IStateAnimationController stateAnimationController,
            IUnitGameObjectController unitGameObjectController,
            IUnitWeapon unitWeapon,
            IMovementController movementController)
        {
            _stateController = stateController;
            _priorityTargetProvider = priorityTargetProvider;
            _followingController = followingController;
            _animationController = stateAnimationController;
            _unitGameObjectController = unitGameObjectController;
            _unitWeapon = unitWeapon;
            _movementController = movementController;
            _randomRangeDeviationPercent = ValueUtility.GetRandom(
                0, Constants.RandomDistancePercentToAttack);
        }


        public override StateType Type
        {
            get { return StateType.UnitIdleState; }
        }

        public override void EnableState()
        {
            _animationController.SetIdleAnimation();
            _unitWeapon.SkillAnimationStarted += OnSkillAnimationStarted;
        }

        public override void ResetState()
        {
            _unitWeapon.SkillAnimationStarted -= OnSkillAnimationStarted;
        }

        private void OnSkillAnimationStarted()
        {
            _stateController.SetAttackState();
        }

        public override void Update()
        {
            var target = _priorityTargetProvider.GetPriorityTarget();
            if (target != null)
            {
                TryAttackTarget(target);
                return;
            }

            if (_followingController.FollowingInstructions != null && _followingController.IsFolowerValid())
            {
                _stateController.SetUnitFollowState();
            }
        }

        private void TryAttackTarget(IStats target)
        {
            var targetUnitPosition = target.GameObjectController.Position;
            _skillForUse = GetSkillForUse();
            var randomRange = GetRandomRange(_skillForUse.General.Range);
            var attackPosition = target.GameObjectController.GetPositionFor(
                _unitGameObjectController.Position.x, randomRange);
            var isCanAttack = IsSkillRanged(_skillForUse)
                ? IsRangedSkillCanAttack(randomRange, target.GameObjectController.Bounds)
                : IsMeleeSkillCanAttack(attackPosition);
            if (isCanAttack)
            {
                _movementController.Stop();
                _unitGameObjectController.TurnUnit(targetUnitPosition.x);

                if (_unitWeapon.UseSkill(_skillForUse, target))
                {
                    _skillForUse = null;
                }
            }
            else
            {
                _movementController.MoveToTargetWithDelay(attackPosition);
            }
        }

        private float GetRandomRange(float range)
        {
            return range - 0.1f - (range * _randomRangeDeviationPercent / 100);
        }

        private ISkillParameters GetSkillForUse()
        {
            if (_skillForUse != null)
            {
                return _skillForUse;
            }
            
            return _unitWeapon.SkillParameters
                .OrderBy(p => _unitWeapon.IsSkillReady(p))
                .ThenByDescending(p => p.General.Range)
                .First();
        }

        private bool IsRangedSkillCanAttack(float range, Bounds targetBounds)
        {
            var distanceToTarget = _unitGameObjectController.GetDistanceTo(targetBounds);
            return range + 0.1 >= distanceToTarget && 
                   distanceToTarget > Constants.MinRangeSkillRange;
        }

        private bool IsMeleeSkillCanAttack(float attackPosition)
        {
            return _unitGameObjectController.IsCollision(attackPosition);
        }

        private bool IsSkillRanged(ISkillParameters skill)
        {
            return skill.General.Range > Constants.MinRangeSkillRange;
        }
    }
}
