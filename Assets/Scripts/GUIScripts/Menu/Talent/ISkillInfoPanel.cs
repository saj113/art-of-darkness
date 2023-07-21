namespace GUIScripts.Menu.Talent
{
    public interface ISkillInfoPanel
    {
        void Show(ISkillInfo skillInfo);
        void Hide();
    }

    public class SkillInfoPanel : ISkillInfoPanel
    {
        private readonly IUIMenuPanel _uiPanel;
        public SkillInfoPanel(
            IUIMenuPanel panel,
            IContextMenuDispatcher contextMenuDispatcher)
        {
            _uiPanel = panel;
            contextMenuDispatcher.CloseAll += Hide;
        }

        public void Show(ISkillInfo skillInfo)
        {
            _uiPanel.Enable();
        }

        public void Hide()
        {
            _uiPanel.Disable();
        }
    }
}
