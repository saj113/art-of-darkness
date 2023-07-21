using Stats;
using Utilities;

namespace GUIScripts
{
    public class HealthBar : StatBarComponent
    {
        protected override void Start()
        {
            base.Start();

            IStats characterStats = FinderUtility.GetPlayerStats(); 
            characterStats.Characteristics.HealthChanged += HandleHealthChanged;
            HandleHealthChanged(characterStats.Characteristics, 0);
        }

        private void HandleHealthChanged(ICharacteristics characteristics, int amount)
        {
            ChangeAmount((float)characteristics.Health / 
                (float)characteristics.MaxHealth);
        }
    }
}
