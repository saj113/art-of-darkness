using Skills;

namespace GUIScripts.Messengers
{
    public interface ISkillUseFailedMessenger
    {
        void ShowMessage(SkillUseFailedReason failedReason);
    }
}