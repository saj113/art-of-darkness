namespace GUIScripts.Menu.Talent
{
    public interface ITalentStatus
    {
        bool IsAvailable { get; set; }
        string UnvailableReason { get; }
    }
}