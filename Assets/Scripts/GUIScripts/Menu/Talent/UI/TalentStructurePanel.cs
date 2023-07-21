using Core;
using UnityEngine;

namespace GUIScripts.Menu.Talent.UI
{
    public class TalentStructurePanel : MonoBehaviour
    {
        [SerializeField] SkillNode _mainSkillNode;
        [SerializeField] SkillNode _bonusSkillNode;
        [SerializeField] SkillNode _extensionLeft1Node;
        [SerializeField] SkillNode _extensionLeft2Node;
        [SerializeField] SkillNode _extensionRight1Node;
        [SerializeField] SkillNode _extensionRight2Node;
        [SerializeField] private SkillInfoPanel _skillInfoPanel;

        private ITalentInfo _talentInfo;
        void Awake()
        {
            Contract.Require(_mainSkillNode != null, "_mainSkillNode");
            Contract.Require(_bonusSkillNode != null, "_bonusSkillNode");
            Contract.Require(_extensionLeft1Node != null, "_extensionLeft1Node");
            Contract.Require(_extensionLeft2Node != null, "_extensionLeft2Node");
            Contract.Require(_extensionRight1Node != null, "_extensionRight1Node");
            Contract.Require(_extensionRight2Node != null, "_extensionRight2Node");
            Contract.Require(_skillInfoPanel != null, "_skillInfoPanel");
            _mainSkillNode.SkillNodeTriggered += ShowSkillInfo;
            _bonusSkillNode.SkillNodeTriggered += ShowSkillInfo;
            _extensionLeft1Node.SkillNodeTriggered += ShowSkillInfo;
            _extensionLeft2Node.SkillNodeTriggered += ShowSkillInfo;
            _extensionRight1Node.SkillNodeTriggered += ShowSkillInfo;
            _extensionRight2Node.SkillNodeTriggered += ShowSkillInfo;
            _skillInfoPanel.Closed += SkillInfoPanelClosed;
        }

        void OnDestroy()
        {
            _mainSkillNode.SkillNodeTriggered -= ShowSkillInfo;
            _bonusSkillNode.SkillNodeTriggered -= ShowSkillInfo;
            _extensionLeft1Node.SkillNodeTriggered -= ShowSkillInfo;
            _extensionLeft2Node.SkillNodeTriggered -= ShowSkillInfo;
            _extensionRight1Node.SkillNodeTriggered -= ShowSkillInfo;
            _extensionRight2Node.SkillNodeTriggered -= ShowSkillInfo;
            _skillInfoPanel.Closed -= SkillInfoPanelClosed;
        }

        public void SetTalentInfo(ITalentInfo talentInfo)
        {
            _talentInfo = talentInfo;
            _mainSkillNode.SetSkillInfo(talentInfo);
            _bonusSkillNode.SetSkillInfo(talentInfo.BonusExtension);
            _extensionLeft1Node.SetSkillInfo(talentInfo.ExtensionLeft1);
            _extensionLeft2Node.SetSkillInfo(talentInfo.ExtensionLeft2);
            _extensionRight1Node.SetSkillInfo(talentInfo.ExtensionRight1);
            _extensionRight2Node.SetSkillInfo(talentInfo.ExtensionRight2);
        }

        private void ShowSkillInfo(ISkillInfo skillInfo)
        {
            _skillInfoPanel.SkillInfoSelected += OnSkillInfoSelected;
            _skillInfoPanel.ShowSkillInfo(skillInfo);
        }

        private void SkillInfoPanelClosed()
        {
            _skillInfoPanel.SkillInfoSelected -= OnSkillInfoSelected;
        }

        private void OnSkillInfoSelected()
        {
            SetTalentInfo(_talentInfo);
        }
    }
}