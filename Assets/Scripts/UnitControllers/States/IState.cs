namespace UnitControllers.States
{
    public interface IState
    {
        StateType Type { get; }
        void EnableState();
        void ResetState();
        void Update();
    }
}
