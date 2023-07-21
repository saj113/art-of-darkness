using UnityEngine;

namespace GUIScripts.Triggers
{
    public interface ISkillTrigger
    {
        void SetSprite(Sprite sprite);
        void UpdateStateByCooldown(float currentCooldown, float setupCooldown);
        void UpdateStateByMana(float fillAmount);
    }
}