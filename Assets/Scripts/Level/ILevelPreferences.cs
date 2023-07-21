using System;

namespace Level
{
    public interface ILevelPreferences
    {
        event Action WallsPositionChanged;
        float InitialLeftBorder { get; }
        float InitialRightBorder { get; }
        float InitialTopBorder { get; }
        float InitialBottomBorder { get; }
        float LeftWall { get; }
        float RightWall { get; }
        float LevelZoneSize { get; }
        float LevelFightZoneRadius { get; }
    }
}