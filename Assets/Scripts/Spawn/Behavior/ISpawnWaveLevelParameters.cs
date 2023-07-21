using Stats;

namespace Spawn.Behavior
{
    public interface ISpawnWaveLevelParameters
    {
        int DistanceBetweenPlayer { get; }
        float RandomDistanceBetweenUnits { get; }
        IStats[] Units { get; }
    }
}
