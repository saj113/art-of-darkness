//using System.Collections.Generic;
//using System.Linq;
//using Assets.Scripts.Core;
//using Assets.Scripts.Core.UnityFramework;
//using Assets.Scripts.StatsComponents;
//using Assets.Scripts.Utilities;

//namespace Assets.Scripts.ControllersComponents.DetectionTargets
//{
//    public class DetectTargetAlgorithm
//    {
//        private readonly IUnitStats _unitStats;
//        private readonly TargetUnitRelation _targetUnitRelation;
//        private readonly IUnityRayController _rayController;
//        private readonly IUnityTimeController _timeController;
//        private readonly List<AggressiveTarget> _aggressiveTargets 
//            = new List<AggressiveTarget>(); 

//        private IStats _currentTarget;

//        private const int TryDetectTargetEveryTime = 1;
//        private float _detectTargetTimeElapsed = 2;

//        public DetectTargetAlgorithm(
//            IUnitStats unitStats,
//            TargetUnitRelation targetUnitRelation,
//            IUnityRayController rayController,
//            IUnityTimeController timeController)
//        {
//            _targetUnitRelation = targetUnitRelation;
//            _rayController = rayController;
//            _timeController = timeController;
//            _unitStats = unitStats;
//            _unitStats.AgrChanged += ChangeAgrFactor;
//            _unitStats.Characteristics.Died += UnitDied;
//        }

//        public IStats GetPriorityTarget()
//        {
//            if (!IsCanDetectTarget())
//            {
//                return _currentTarget;
//            }

//            UpdateTargetCollection();

//            if (!_aggressiveTargets.Any())
//            {
//                return null;
//            }

//            _aggressiveTargets.Sort();
//            var priorityeTarget = _aggressiveTargets.First().Target;
//            if (!Equals(priorityeTarget, _currentTarget))
//            {
//                if (_currentTarget != null)
//                {
//                    _currentTarget.DequeueFromAttack();
//                }
//                priorityeTarget.EnqueueToAttack();
//            }

//            _currentTarget = priorityeTarget;
//            return priorityeTarget;
//        }

//        private void UpdateTargetCollection()
//        {
//            var nearestTargets = GetNearestValidTargets();
//            foreach (var nearestTarget in nearestTargets)
//            {
//                if (FindAggressiveTargetByStats(nearestTarget) == null)
//                {
//                    _aggressiveTargets.Add(
//                        new AggressiveTarget(_unitStats, nearestTarget));
//                    nearestTarget.Characteristics.Died += TargetStatsOnDied;
//                }
//            }
//        }

//        private bool IsCanDetectTarget()
//        {
//            _detectTargetTimeElapsed += _timeController.DeltaTime;
//            if (_detectTargetTimeElapsed > TryDetectTargetEveryTime)
//            {
//                _detectTargetTimeElapsed = 0;
//                return true;
//            }

//            return false;
//        }

//        private void TargetStatsOnDied(ICharacteristics characteristics)
//        {
//            characteristics.Died -= TargetStatsOnDied;
//            var aggressiveTarget = _aggressiveTargets.Find(p =>
//                p.Target.Characteristics.Equals(characteristics));
//            _aggressiveTargets.Remove(aggressiveTarget);

//            if (_currentTarget != null && _currentTarget.Characteristics.Equals(characteristics))
//            {
//                _currentTarget.DequeueFromAttack();
//                _currentTarget = null;
//                _detectTargetTimeElapsed = 2;
//            }
//        }

//        private void ChangeAgrFactor(ICharacteristics characteristics, int amount)
//        {
//            if (!IsTargetValid(characteristics))
//            {
//                return;
//            }

//            var agrTarget = FindAggressiveTargetByStats(characteristics);
//            if (agrTarget == null)
//            {
//                agrTarget = new AggressiveTarget(_unitStats, characteristics);
//                _aggressiveTargets.Add(agrTarget);
//            }

//            agrTarget.IncreaseAgr(amount);
//        }

//        private bool IsTargetValid(ICharacteristics characteristics)
//        {
//            return ConditionUtility.IsTargetValid(_unitStats, characteristics, _targetUnitRelation);
//        }

//        private IEnumerable<IStats> GetNearestValidTargets()
//        {
//            return _rayController.CastToUnits(_unitStats.GameObjectController.CenterPosition, ConstantUtility.DetectTargetDistance)
//                .Where(p => IsTargetValid(p.Characteristics));
//        }

//        private void UnitDied(ICharacteristics characteristics)
//        {
//            _unitStats.Characteristics.Died -= UnitDied;
//            if (_currentTarget != null)
//            {
//                _currentTarget.DequeueFromAttack();
//                _currentTarget = null;
//            }
//        }

//        private AggressiveTarget FindAggressiveTargetByStats(ICharacteristics characteristics)
//        {
//            return _aggressiveTargets.FirstOrDefault(p => p.Target.Characteristics.Equals(characteristics));
//        }
//    }
//}

namespace UnitControllers.DetectionTargets
{
}