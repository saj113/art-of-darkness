namespace GUIScripts.Menu.Talent
{
    public class TalentStatus : ITalentStatus
    {
        public TalentStatus(bool isAvailable, string unvailableReason)
        {
            IsAvailable = isAvailable;
            UnvailableReason = unvailableReason;
        }

        public bool IsAvailable {get; set;}

        public string UnvailableReason {get; private set;}
    }
}