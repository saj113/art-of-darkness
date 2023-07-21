using Skills;
using UnityEngine;
using UnityEngine.UI;

namespace GUIScripts.Messengers
{
    [RequireComponent(typeof(Text))]
    public class SkillUseFailedMessenger : MonoBehaviour, ISkillUseFailedMessenger
    {
        private Text _text;
        private float _displayedTime;
        
        public void ShowMessage(SkillUseFailedReason failedReason)
        {
            var message = TextProvider.GetSkillCooldownFailedMessage(failedReason);
            _text.text = message;
            _text.enabled = true;
            _displayedTime = 0;
        }

        void Start()
        {
            _text = gameObject.GetComponent<Text>();
        }

        void Update()
        {
            if (_text.enabled)
            {
                if (_displayedTime < 3)
                {
                    _displayedTime += Time.deltaTime;
                }

                if (_displayedTime >= 3)
                {
                    _text.enabled = false;
                }
            }
        }
    }
}