using System;

namespace Stats
{
    public interface ICharacteristics : IDisposable
    {
        Tag Tag { get; set; }
        Guid UnitId { get; }
        int Health { get; }
        int Mana { get; }
        int MaxHealth { get; }
        int MentalPower { get; }
        float MovementSpeed { get; }
        int Power { get; }
        bool HasResistToSlowdown { get; }
        bool HasResistToStandUp { get; }
        bool HasResistToStun { get; }
        bool IsFacingRight { get; set; }
        event Action<Tag> TagChanged; 
        event Action<ICharacteristics> Died;
        event Action<ICharacteristics, int> HealthChanged;
        event Action<ICharacteristics, int> ManaChanged; 
        void Kill();
        void ChangeStat(StatAttribute statAttribute, int amount);
        void IncreaseStatByPercent(StatAttribute statAttribute, int percent);
        void DecreaseStatByPercent(StatAttribute statAttribute, int percent);
    }
}