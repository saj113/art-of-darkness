using Core;
using Core.Provider;
using Stats;
using Utilities;

namespace UnitControllers.DetectionTargets
{
    internal class PriorityTargetProvider : IPriorityTargetProvider
    {
        private readonly ISkillCaster _caster;
        private readonly ITargetUnitProvider _targetUnitProvider;
        private readonly IDetectTargetAlgorithm _strategy;
        private readonly Timer _timer;
        private IStats _priorityTarget;
        private ILogger _logger;

        public PriorityTargetProvider(ISkillCaster caster,
                                      ITargetUnitProvider targetUnitProvider,
                                      IDetectTargetAlgorithm strategy,
                                      ILogger logger)
        {
            _strategy = strategy;
            _targetUnitProvider = targetUnitProvider;
            _caster = caster;
            _timer = new Timer(3);
            _logger = logger;
        }

        public IStats GetPriorityTarget()
        {
            if (_timer.IsCharged() ||
                (_priorityTarget != null && _priorityTarget.Characteristics.Health == 0) ||
                (_priorityTarget != null &&
                 !ConditionUtility.CheckUnitRelationship(_caster.Characteristics.Tag,
                                                        _priorityTarget.Characteristics.Tag,
                                                        _caster.TargetRelation)))
            {
                FindTarget();
            }

            return _priorityTarget;
        }

        private void FindTarget()
        {
            var targets = _targetUnitProvider.Get(_caster.Characteristics.Tag, 
                                                  _caster.TargetRelation);
            _priorityTarget = _strategy.GetPriorityTarget(
                    _caster.GameObjectController.CenterPosition,
                    targets);
        }
    }
}
