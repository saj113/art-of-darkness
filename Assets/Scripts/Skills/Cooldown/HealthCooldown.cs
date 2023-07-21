using Core;
using Stats;
using Utilities;

namespace Skills.Cooldown
{
    public class HealthCooldown : ISkillCooldown
    {
        private readonly ICharacteristics _characteristics;
        private readonly int _requiredHealth;

        public HealthCooldown(ICharacteristics characteristics, int requiredHealthInPercent)
        {
            _characteristics = characteristics;
            _requiredHealth =  ValueUtility.CalculatePercent(
                _characteristics.MaxHealth, requiredHealthInPercent);
            Contract.Ensure(requiredHealthInPercent > 0, "RequiredHealth has invalid value");
        }
        
        public SkillCooldownType Type => SkillCooldownType.Health;

        public bool IsReady()
        {
            return _requiredHealth < _characteristics.Health;
        }

        public void GiveCost()
        {
            _characteristics.ChangeStat(StatAttribute.Health, -_requiredHealth);
        }

        public void Dispose()
        {
        }
    }
}