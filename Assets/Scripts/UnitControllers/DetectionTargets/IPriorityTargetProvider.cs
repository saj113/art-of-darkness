using Stats;

namespace UnitControllers.DetectionTargets
{
    public interface IPriorityTargetProvider
    {
        IStats GetPriorityTarget();
    }
}