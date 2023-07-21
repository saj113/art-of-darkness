using System;
using System.Linq;
using Core;

namespace Level.SpawnEnemies.Models
{
    public class SpawnLevel : ISpawnLevel
    {
        public SpawnLevel(int levelWeigh, IWaveModel[] waveModels, IUnitType[] unitTypes)
        {
            unitTypes.ThrowIfNull(nameof(unitTypes));
            if (unitTypes.All(p => p.Weigh != 1))
            {
                throw new ArgumentException("Unit Types must haves at least one type with weigh '1'");
            }

            LevelWeigh = levelWeigh;
            WaveModels = waveModels.ThrowIfNull(nameof(waveModels));
            UnitTypes = unitTypes;
        }

        public int LevelWeigh { get; }
        public IWaveModel[] WaveModels { get; }
        public IUnitType[] UnitTypes { get; }
    }
}