using System;
using System.Collections.Generic;
using Core.Prefabs;
using Level.SpawnEnemies.Models;

namespace Level.SpawnEnemies
{
    public class SpawnLevelContainer : ILevelContainer
    {
        private const int UnitSoldierWeigh = 1;
        private const int UnitMageWeigh = 1;
        private const int UnitAdvancedSoldierWeigh = 2;
        private const int UnitAdvancedMageWeigh = 2;
        private const int UnitDogWeigh = 1;
        
        private readonly IUnitPrefabProvider _unitPrefabProvider;

        public SpawnLevelContainer(IUnitPrefabProvider unitPrefabProvider)
        {
            _unitPrefabProvider = unitPrefabProvider;
        }
        
        public ISpawnLevel GetByPlayerLevel(int playerLevel)
        {
            switch (playerLevel)
            {
                case 1: return CreateLevel1();
            }

            throw new NotSupportedException($"Player level {playerLevel} isn't supported");
        }

        private ISpawnLevel CreateLevel1()
        {
            var levelWeigh = 30;
            var waveWeighPercents = new IWaveModel[]
            {
                new WaveModel(20, new []{ 20, 50, 30 }), 
                new WaveModel(20, new []{ 50, 50 }), 
                new WaveModel(60, new []{ 20, 20, 60 }), 
            };
            var unitTypes = new IUnitType[]
            {
                new UnitType(1, UnitSoldierWeigh, _unitPrefabProvider.GetLevel1SimpleSoldier()), 
                new UnitType(1, UnitMageWeigh, _unitPrefabProvider.GetLevel1SimpleMage()), 
                new UnitType(1, UnitDogWeigh, _unitPrefabProvider.GetLevel1Dog()) 
            };
            
            return new SpawnLevel(levelWeigh, waveWeighPercents, unitTypes);
        }
    }
}