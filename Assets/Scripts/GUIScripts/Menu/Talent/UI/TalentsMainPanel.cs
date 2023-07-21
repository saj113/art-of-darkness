using System;
using Core;
using UnityEngine;

namespace GUIScripts.Menu.Talent.UI
{
    public class TalentsMainPanel : MonoBehaviour
    {

        [SerializeField] private ContextMenuPanel _contextMenuPanel;
        [SerializeField] private TalentsListPanel _talentsListPanel;
        [SerializeField] private SkillInfoPanel _skillInfoPanel;
        private TalentInfoCollectionProvider _talentInfoCollectionProvider = new TalentInfoCollectionProvider();

        public event Action Closed;

        void Awake()
        {
            Contract.Require(_contextMenuPanel != null, "_contextMenuPanel");
            Contract.Require(_contextMenuPanel != null, "_talentsListPanel");
            Contract.Require(_contextMenuPanel != null, "_skillInfoPanel");
            _contextMenuPanel.Closed += Close;
            _skillInfoPanel.Close();
            gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            _contextMenuPanel.Closed -= Close;
        }

        public void Open()
        {
            _talentsListPanel.LoadTalents(_talentInfoCollectionProvider.GetMeleeTalentInfoCollection());
            gameObject.SetActive(true);
        }

        private void Close()
        {
            _skillInfoPanel.Close();
            gameObject.SetActive(false);
            OnClose();
        }

        private void ShowSkillInfo(ISkillInfo skillInfo)
        {
            _skillInfoPanel.ShowSkillInfo(skillInfo);
            _skillInfoPanel.gameObject.SetActive(true);
        }

        private void OnClose()
        {
            var handler = Closed;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
