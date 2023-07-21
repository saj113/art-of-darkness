using Stats;

namespace Skills.Modificators
{
    public interface IModificator
    {
        bool IsBuff { get; }
        void Apply(IStats target);
    }
}
