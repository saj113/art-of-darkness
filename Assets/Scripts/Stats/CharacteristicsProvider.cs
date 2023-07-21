using System;
using Core.UnityFramework;
using UnitControllers;

namespace Stats
{
    public class CharacteristicsProvider
    {
        public ICharacteristics GetCharacteristics(
            int level,
            Guid unitId,
            CharacteristicsTemplate template, 
            Tag tag,
            IAbsorbingBarrierController absorbingBarrierController,
            IUnityUpdateEvents updateEvents)
        {
            switch (template)
            {
                case CharacteristicsTemplate.Necromancer:
                    return new CharacterCharacteristics(
                        unitId,
                        tag,
                        StatsLevelIncreaser.IncreaseHealth(1000, level),
                        StatsLevelIncreaser.IncreasePower(100, level),
                        4f,
                        absorbingBarrierController,
                        updateEvents);

                case CharacteristicsTemplate.Summon:
                    return new Characteristics(
                        unitId,
                        tag,
                        StatsLevelIncreaser.IncreaseHealth(50, level),
                        StatsLevelIncreaser.IncreasePower(10, level),
                        3,
                        absorbingBarrierController,
                        hasResistToStandUp: true,
                        hasResistToSlowdown: true,
                        hasResistToStun: true);
                
                case CharacteristicsTemplate.Dog:
                    return new Characteristics(
                        unitId,
                        tag,
                        StatsLevelIncreaser.IncreaseHealth(120, level),
                        StatsLevelIncreaser.IncreasePower(25, level),
                        10,
                        absorbingBarrierController,
                        hasResistToSlowdown: true);
                
                case CharacteristicsTemplate.SimpleSoldier:
                    return new Characteristics(
                        unitId,
                        tag,
                        StatsLevelIncreaser.IncreaseHealth(150, level),
                        StatsLevelIncreaser.IncreasePower(25, level),
                        5,
                        absorbingBarrierController);
                
                case CharacteristicsTemplate.AdvancedSoldier:
                    return new Characteristics(
                        unitId,
                        tag,
                        StatsLevelIncreaser.IncreaseHealth(250, level),
                        StatsLevelIncreaser.IncreasePower(40, level),
                        7f,
                        absorbingBarrierController);
                case CharacteristicsTemplate.AdvancedShieldSoldier:
                    return new Characteristics(
                        unitId,
                        tag,
                        StatsLevelIncreaser.IncreaseHealth(400, level),
                        StatsLevelIncreaser.IncreasePower(25, level),
                        5f,
                        absorbingBarrierController);

                case CharacteristicsTemplate.SimpleMage:
                    return new Characteristics(
                        unitId,
                        tag,
                        StatsLevelIncreaser.IncreaseHealth(100, level),
                        StatsLevelIncreaser.IncreasePower(20, level),
                        3f,
                        absorbingBarrierController);
                
                case CharacteristicsTemplate.AdvancedMage:
                    return new Characteristics(
                        unitId,
                        tag,
                        StatsLevelIncreaser.IncreaseHealth(200, level),
                        StatsLevelIncreaser.IncreasePower(40, level),
                        4f,
                        absorbingBarrierController);
                
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
