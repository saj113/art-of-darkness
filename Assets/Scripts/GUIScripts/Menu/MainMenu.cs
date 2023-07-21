using Core;
using GUIScripts.Menu.Talent.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUIScripts.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private EventTrigger _openTalentMenuTrigger;
        [SerializeField]
        private TalentsMainPanel _talentMainPanel;

        void Awake()
        {
            Contract.Require(_openTalentMenuTrigger != null, "_openTalentMenuTrigger == null");
            Contract.Require(_talentMainPanel != null, "_talentMainPanel == null");
            _openTalentMenuTrigger.Register(EventTriggerType.PointerDown, OpenTalentMenu);
            _talentMainPanel.Closed += TalentMenuClosed;
        }

        private void OpenTalentMenu()
        {
            _openTalentMenuTrigger.enabled = false;
            _talentMainPanel.Open();


        }

        private void TalentMenuClosed()
        {
            _openTalentMenuTrigger.enabled = true;
        }
    }
}