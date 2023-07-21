namespace Skills.Parameters.ModificatorParameters
{
    public interface IBehaviorModificatorsBuffParameters : IModificatorsBuffParameters
    {
        IColliderParameters ColliderParameters { get; }
    }
}