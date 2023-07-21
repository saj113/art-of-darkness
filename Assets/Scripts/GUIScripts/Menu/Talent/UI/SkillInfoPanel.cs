using System;
using Core;
using Store;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUIScripts.Menu.Talent.UI
{
    public class SkillInfoPanel : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Text _nameText;
        [SerializeField]
        private Text _descText;
        [SerializeField]
        private Text _reqText;
        
        [SerializeField]
        private EventTrigger _selectSkillTrigger;
        [SerializeField]
        private EventTrigger _closeTrigger;
        public event Action Closed;
        public event Action SkillInfoSelected;
        private ISkillInfo _skillInfo;
        public void ShowSkillInfo(ISkillInfo skillInfo)
        {
            _skillInfo = skillInfo;
            _image.sprite = _skillInfo.Icon;
            _nameText.text = _skillInfo.Name;
            _descText.text = _skillInfo.Description;
            if (_skillInfo.Status.IsAvailable)
            {
                _reqText.enabled = true;
                _reqText.text = _skillInfo.Status.UnvailableReason;
            }
            else
            {
                _reqText.enabled = false;
            }
            gameObject.SetActive(true);
        }

        public void Close()
        {
            OnClose();
            gameObject.SetActive(false);
        }

        private void SelectSkillInfo()
        {
            if (!_skillInfo.Status.IsAvailable) return;

            Repository.Instance.SelectTalent(_skillInfo.Key);
            _skillInfo.Status.IsAvailable = true;

            OnSkillInfoSelected();
            Close();
        }

        private void OnSkillInfoSelected()
        {
            var handler = SkillInfoSelected;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnClose()
        {
            var handler = Closed;
            if (handler != null)
            {
                handler();
            }
        }

        void Awake()
        {
            Contract.Require(_image != null, "_image == null");
            Contract.Require(_nameText != null, "_nameText == null");
            Contract.Require(_descText != null, "_descText == null");
            Contract.Require(_reqText != null, "_reqText == null");
            Contract.Require(_selectSkillTrigger != null, "_selectSkillTrigger == null");
            Contract.Require(_closeTrigger != null, "_closeTrigger == null");

            _selectSkillTrigger.Register(EventTriggerType.PointerDown, SelectSkillInfo);
            _closeTrigger.Register(EventTriggerType.PointerDown, Close);
        }
    }
}
