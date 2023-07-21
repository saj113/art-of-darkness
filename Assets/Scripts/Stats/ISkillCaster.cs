using Skills;
using UnitControllers;

namespace Stats
{
    public interface ISkillCaster
    {
        TargetUnitRelation TargetRelation { get; }
        ICharacteristics Characteristics { get; }
        IAcolyteController AcolyteController { get; }
        IUnitGameObjectController GameObjectController { get; }
        IAgrController AgrController { get; }
    }
}
