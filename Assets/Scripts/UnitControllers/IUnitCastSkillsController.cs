using Stats;

namespace UnitControllers
{
    public interface IUnitCastSkillsController
    {
        bool TryCast(IStats target);
    }
}