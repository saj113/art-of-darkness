using Core;

namespace Level.SpawnEnemies.Models
{
    public class WaveModel : IWaveModel
    {
        public WaveModel(int weighPercent, int[] subWeighPercents)
        {
            WeighPercent = weighPercent;
            SubWeighPercents = subWeighPercents.ThrowIfNull(nameof(subWeighPercents));
        }

        public int WeighPercent { get; }
        public int[] SubWeighPercents { get; }
    }
}