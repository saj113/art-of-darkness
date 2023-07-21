using System;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUIScripts.Menu.Talent.UI
{
    [RequireComponent(typeof(EventTrigger))]
    [RequireComponent(typeof(Image))]
    public class SkillNode : MonoBehaviour
    {
        private ISkillInfo _skillInfo;
        public event Action<ISkillInfo> SkillNodeTriggered;
        public void SetSkillInfo(ISkillInfo skillInfo)
        {
            _skillInfo = skillInfo;
            GetComponent<Image>().sprite = _skillInfo.Icon;
        }

        void Awake()
        {
            GetComponent<EventTrigger>().Register(EventTriggerType.PointerDown, OnSkillInfoTriggered);
        }

        private void OnSkillInfoTriggered()
        {
            var handler = SkillNodeTriggered;
            if (handler != null)
            {
                handler(_skillInfo);
            }
        }
    }
}