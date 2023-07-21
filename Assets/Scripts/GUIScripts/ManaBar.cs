using Stats;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace GUIScripts
{
    [RequireComponent(typeof(Text))]
    public class ManaBar : StatBarComponent
    {
        private Text _text;
        protected override void Start()
        {
            base.Start();
            _text = GetComponentInChildren<Text>();

            var characteristics = FinderUtility.GetPlayerStats().Characteristics; 
            characteristics.ManaChanged += HandleManaChanged;
            HandleManaChanged(characteristics, 0);
        }

        private void HandleManaChanged(ICharacteristics characteristics, int amount)
        {
            _text.text = characteristics.Mana.ToString();
            ChangeAmount((float)characteristics.Mana / 100);
        }
    }
}