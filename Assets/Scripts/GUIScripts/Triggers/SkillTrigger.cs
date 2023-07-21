using System;
using System.Globalization;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace GUIScripts.Triggers
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class SkillTrigger : MonoBehaviour, ISkillTrigger
    {
        [SerializeField] private Text _cooldownText;
        private Image _skillImage;
        [SerializeField] private Image _notReadyImage;
        [SerializeField] private Image _notEnoughManaImage;

        protected virtual void Awake()
        {
            _skillImage = GetComponent<Image>();
            Contract.Ensure(_skillImage != null, "Skill Image component is not set");
            Contract.Ensure(_cooldownText != null, "Cooldown Text component is not set");
            Contract.Ensure(_notReadyImage != null, "Not Ready Image component is not set");
            Contract.Ensure(_notEnoughManaImage != null, "Not Enough Mana Image component is not set");

            _skillImage.preserveAspect = true;
            _notReadyImage.preserveAspect = true;
            _notEnoughManaImage.preserveAspect = true;
            _notReadyImage.type = Image.Type.Filled;
            _notReadyImage.fillAmount = 0;
            _notReadyImage.fillClockwise = false;
            _notReadyImage.fillMethod = Image.FillMethod.Radial360;
            _notReadyImage.fillOrigin = 2;
            _notEnoughManaImage.type = Image.Type.Filled;
            _notEnoughManaImage.fillAmount = 0;
        }

        public void SetSprite(Sprite sprite)
        {
            _skillImage.sprite = sprite;
            _notReadyImage.sprite = sprite;
            _notEnoughManaImage.sprite = sprite;
        }

        public void UpdateStateByCooldown(float currentCooldown, float setupCooldown)
        {
            var cooldownExist = currentCooldown > 0.01f;
            _notReadyImage.fillAmount = !cooldownExist ? 0 : currentCooldown / setupCooldown;
            _cooldownText.enabled = cooldownExist;
            if (cooldownExist)
            {
                _cooldownText.text = Math.Ceiling(currentCooldown).ToString(CultureInfo.InvariantCulture);
            }
        }

        public void UpdateStateByMana(float fillAmount)
        {
            _notEnoughManaImage.fillAmount = fillAmount;
        }
    }
}