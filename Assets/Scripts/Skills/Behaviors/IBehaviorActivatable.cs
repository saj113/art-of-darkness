namespace Skills.Behaviors
{
    public interface IBehaviorActivatable
    {
        void Activate();
        void FinishActivation();
        bool IsActivatable(out SkillUseFailedReason failedReason);
    }
}
