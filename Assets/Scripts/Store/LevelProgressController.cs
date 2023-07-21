using System.Collections.Generic;

namespace Store
{
    public class LevelProgressController : ILevelProgressController
    {
        private readonly IDictionary<int, int> _missionsCountByLevel;
        private readonly IRepository _repository;

        public LevelProgressController(IRepository repository)
        {
            _repository = repository;
            _missionsCountByLevel = GetMissionsCountByLevel();
        }

        public void NotifyMissionCompleted()
        {
            if (_repository.NextMission == _missionsCountByLevel[_repository.Level])
            {
                _repository.GoToNextMission(0);
                _repository.LevelUp();
            }
            else
            {
                _repository.GoToNextMission(_repository.NextMission + 1);
            }
        }

        private IDictionary<int, int> GetMissionsCountByLevel()
        {
            return new Dictionary<int, int>()
            {
                {1,3},
                {2,3},
                {3,3},
                {4,3},
                {5,5}
            };
        }
    }

    public interface ILevelProgressController
    {
        void NotifyMissionCompleted();
    }
}