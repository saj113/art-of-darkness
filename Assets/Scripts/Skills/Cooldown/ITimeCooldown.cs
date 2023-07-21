namespace Skills.Cooldown
{
    public interface ITimeCooldown
    {
        float CurrentCooldown { get; }
        float RequiredCooldown { get; }
    }
}