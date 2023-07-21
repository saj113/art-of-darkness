using Skills;

namespace Store
{
    public interface IRepository: ITalentSelecter
    {
        int Level { get; }
        int AvailablePoints { get; }
        int NextMission { get; }
        bool IsTalentSelected(TalentNameKey key);
        void LevelUp();
        void GoToNextMission(int newValue);
    }

    public interface ITalentSelecter
    {
        void SelectTalent(TalentNameKey key);
    }
}