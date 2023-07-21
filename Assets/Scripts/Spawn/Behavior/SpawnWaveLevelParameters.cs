using Stats;

namespace Spawn.Behavior
{
    public class SpawnWaveLevelParameters : ISpawnWaveLevelParameters
    {
        private readonly int _distanceBetweenPlayer;
        private readonly float _randomDistanceBetweenUnits;
        private readonly IStats[] _units;

        public SpawnWaveLevelParameters(
            int distanceBetweenPlayer, float randomDistanceBetweenUnits, IUnitStats[] units)
        {
            _units = units;
            _distanceBetweenPlayer = distanceBetweenPlayer;
            _randomDistanceBetweenUnits = randomDistanceBetweenUnits;
        }

        public int DistanceBetweenPlayer
        {
            get { return _distanceBetweenPlayer; }
        }

        public float RandomDistanceBetweenUnits
        {
            get { return _randomDistanceBetweenUnits; }
        }

        public IStats[] Units
        {
            get { return _units; }
        }
    }
}
