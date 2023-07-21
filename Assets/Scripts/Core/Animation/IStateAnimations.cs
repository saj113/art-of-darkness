namespace Core.Animation
{
    public interface IStateAnimations
    {
        string Idle { get; }
        string Fall { get; }
        string Run { get; }
        string Stun { get; }
        string StandUp { get; }
        float RunTimeScale { get; }
    }
}