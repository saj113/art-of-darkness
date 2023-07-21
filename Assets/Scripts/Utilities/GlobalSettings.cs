using System.Collections.Generic;

namespace Utilities
{
    internal static class GlobalSettings
    {
        public static readonly IEnumerable<LayerInfo> LayersInfo;
        public static readonly IncreaseStatsPercentAmounts IncreaseStatsPercentAmounts;
        public static readonly IncreaseExperienceInfo IncreaseExperienceInfo;
        static GlobalSettings()
        {
            LayersInfo = new List<LayerInfo>
            {
                new LayerInfo(1, 6, 10, 10),
                new LayerInfo(2, 6, 12, 12),
                new LayerInfo(3, 6, 6, 6)
            };

            IncreaseStatsPercentAmounts = new IncreaseStatsPercentAmounts
            {
                Health = 20, Power = 15
            };

            IncreaseExperienceInfo = new IncreaseExperienceInfo
            {
                StartlevelUpExperience = 100,
                LayerGainInExperience = 2f,
                QuestsExperienceAmoutPercent = 30,
                BossExperienceAmoutPercent = 20,
                SublayersExperienceAmoutPercent = 50
            };
        }
    }

    public class LayerInfo
    {
        public int LayerIndex { get; private set; }
        public int UnitLavelCount { get; private set; }
        public int QuestCount { get; private set; }
        public int SublayertCount { get; private set; }

        public LayerInfo(
            int layerIndex, 
            int unitLavelCount, 
            int questCount, 
            int sublayerCount)
        {
            LayerIndex = layerIndex;
            UnitLavelCount = unitLavelCount;
            QuestCount = questCount;
            SublayertCount = sublayerCount;
        }
    }

    public struct IncreaseExperienceInfo
    {
        public int StartlevelUpExperience { get; set; }
        public float LayerGainInExperience { get; set; }
        public int QuestsExperienceAmoutPercent { get; set; }
        public int BossExperienceAmoutPercent { get; set; }
        public int SublayersExperienceAmoutPercent { get; set; }
    }

    public struct IncreaseStatsPercentAmounts
    {
        public int Health { get; set; }
        public int Power { get; set; }
    }
}
