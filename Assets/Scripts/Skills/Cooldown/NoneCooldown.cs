namespace Skills.Cooldown
{
    public class NoneCooldown : ISkillCooldown
    {
        public SkillCooldownType Type => SkillCooldownType.None;
        public bool IsReady()
        {
            return true;
        }

        public void GiveCost()
        {
            
        }

        public void Dispose()
        {
        }
    }
}