using Stats;

namespace Skills.Parameters.BehaviorParameters
{
    public interface IModificatorsApplier
    {
        void ApplyModificators(IStats target, bool applyBuffs);
    }
}
