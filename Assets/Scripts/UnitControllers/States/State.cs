namespace UnitControllers.States
{
    public abstract class State : IState
    {
        public abstract StateType Type { get; }

        public virtual void EnableState()
        {

        }

        public virtual void ResetState()
        {

        }

        public virtual void Update()
        {

        }
    }
}
