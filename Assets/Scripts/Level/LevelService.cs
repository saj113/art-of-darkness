using System;

namespace Level
{
    public class LevelService : ILevelService, ILevelPreferences
    {
        private const float FightZoneRadius = 100;
        
        public LevelService()
        {
            SetInitialLevelScope();
        }

        public event Action WallsPositionChanged;
        public float InitialLeftBorder => -50;
        public float InitialRightBorder => 1000;
        public float InitialTopBorder => 30;
        public float InitialBottomBorder => -10;
        public float LeftWall { get; private set; }
        public float RightWall { get; private set; }
        public float LevelZoneSize
        {
            get { return InitialRightBorder; }
        }
        
        public float LevelFightZoneRadius
        {
            get { return FightZoneRadius; }
        }

        public void EnableFightZone(float playerPosition)
        {
            LeftWall = playerPosition - FightZoneRadius;
            RightWall = playerPosition + FightZoneRadius;
            OnBordersChanged();
        }

        public void DisableFightZone()
        {
            SetInitialLevelScope();
        }

        public void FinishLevel(float playerPosition)
        {
            throw new System.NotImplementedException();
        }

        public void CancelLevel()
        {
            throw new System.NotImplementedException();
        }

        private void SetInitialLevelScope()
        {
            LeftWall = InitialLeftBorder;
            RightWall = InitialRightBorder;
            OnBordersChanged();
        }

        protected virtual void OnBordersChanged()
        {
            WallsPositionChanged?.Invoke();
        }
    }
}
