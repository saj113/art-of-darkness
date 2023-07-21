namespace Skills.Parameters.BehaviorParameters
{
    public interface IBehaviorsParameters : IParticlesFromCasterParameters, IParticlesToCasterParameters,
        IRayFromCasterParameters, ISummonAcolyteFromCasterParameters, ISummonColliderFromCasterParameters,
        ISummonColliderFromPointParameters, IAffectToUnitParameters, IRayFromPointParameters,
        IParticlesToCasterFromTargetsParameters, ISummonSupportedColliderFromCasterParameters
    {
        new SkillBehaviorType Type { get; }
    }
}