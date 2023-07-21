using Level.SpawnEnemies.Models;

namespace Level.SpawnEnemies
{
    public interface ILevelContainer
    {
        ISpawnLevel GetByPlayerLevel(int playerLevel);
    }
}