namespace Level
{
    public interface ILevelService
    {
        void EnableFightZone(float playerPosition);
        void DisableFightZone();
        void FinishLevel(float playerPosition);
        void CancelLevel();
    }
}