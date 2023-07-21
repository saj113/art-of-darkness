using Core;

namespace Skills.Parameters.ModificatorParameters
{
    public interface IModificatorsBuffParameters : IAbsorbShieldBuffParameters,
        IChangeStatsBuffParameters,
        IDoTBuffParameters,
        IEnslaveBuffParameters,
        IFearBuffParameters,
        IFlyBuffParameters,
        IRegenBuffParameters,
        ISlowdownBuffParameters,
        IStunBuffParameters,
        IChangeTagBuffParameters
    {
        new int Power {get;}
        ModificatorType Type { get; }
        TargetUnitRelation TargetRelation { get; }
        int AgrFactorMultiplication { get; }
        int DamagePowerToCaster { get; }
        float Radius { get; }
        Direction Direction { get; }
        float ThrowingYCoordinate { get; }
        float ThrowingThrust { get; }
    }
}
