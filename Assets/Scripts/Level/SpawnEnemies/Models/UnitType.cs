using System;
using Core;
using Stats.Data;

namespace Level.SpawnEnemies.Models
{
    public class UnitType : IUnitType
    {
        public UnitType(int playerLevel, int weigh, UnitStatsData unitPrefab)
        {
            if (playerLevel < 1) throw new ArgumentException("Player level is invalid");
            if (weigh < 1) throw new ArgumentException("Weigh is invalid");
            
            UnitPrefab = unitPrefab.ThrowIfNull(nameof(unitPrefab));
            Weigh = UnitPrefab.Level > playerLevel ? weigh * 2 : weigh;
        }

        public int Weigh { get; }
        public UnitStatsData UnitPrefab { get; }
    }
}