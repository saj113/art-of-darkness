using Core;
using Stats;

namespace Skills.Cooldown
{
    public class ManaCooldown : ISkillCooldown
    {
        private readonly ICharacteristics _characteristics;
        private readonly int _requiredMana;

        public ManaCooldown(ICharacteristics characteristics, int requiredMana)
        {
            _characteristics = characteristics;
            _requiredMana = requiredMana;
            Contract.Ensure(requiredMana > 0, "RequiredMana has invalid value");
        }
        
        public SkillCooldownType Type => SkillCooldownType.Mana;

        public bool IsReady()
        {
            return _requiredMana <= _characteristics.Mana;
        }

        public void GiveCost()
        {
            _characteristics.ChangeStat(StatAttribute.Mana, -_requiredMana);
        }

        public void Dispose()
        {
        }
    }
}