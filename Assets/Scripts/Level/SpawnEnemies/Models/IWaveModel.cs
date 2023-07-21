namespace Level.SpawnEnemies.Models
{
    public interface IWaveModel
    {
        int WeighPercent { get; }
        int[] SubWeighPercents { get; }
    }
}