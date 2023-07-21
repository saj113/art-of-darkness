using Skills.Modificators;
using Stats;

namespace Skills.Parameters
{
    public interface IModificatorParametersProvider
    {
        IModificator GetModificator(ISkillCaster caster);
    }
}
