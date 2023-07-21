using System;
using UnitControllers;
using UnityEngine;
using Utilities;

namespace Stats
{
    public class Characteristics : ICharacteristics
    {
        private readonly IAbsorbingBarrierController _absorbingBarrierController;
        
        private int _defensePercent;
        private readonly int _basePower;
        private readonly int _baseMentalPower;
        private readonly int _baseDefensePercent;
        private Tag _tag;

        public Characteristics(
            Guid unitId,
            Tag tag,
            int health,
            int power,
            float speed,
            IAbsorbingBarrierController absorbingBarrierController,
            int defensePercent = 0,
            int mentalPower = 0,
            bool hasResistToStandUp = false,
            bool hasResistToSlowdown = false,
            bool hasResistToStun= false)
        {
            UnitId = unitId;
            _tag = tag;
            IsFacingRight = true;
            MaxHealth = health;
            Health = health;
            Power = power;
            _defensePercent = defensePercent;
            MentalPower = mentalPower;
            Mana = 0;
            MovementSpeed = speed;
            HasResistToSlowdown = hasResistToSlowdown;
            HasResistToStandUp = hasResistToStandUp;
            HasResistToStun = hasResistToStun;

            _absorbingBarrierController = absorbingBarrierController;
            _baseDefensePercent = defensePercent;
            _basePower = power;
            _baseMentalPower = mentalPower;
        }

        public event Action<ICharacteristics, int> HealthChanged;
        public event Action<Tag> TagChanged;
        public event Action<ICharacteristics> Died;
        public event Action<ICharacteristics, int> ManaChanged;

        public Tag Tag
        {
            get { return _tag; }
            set
            {
                var oldTag = _tag;
                _tag = value;
                OnTagChanged(oldTag);
            }
        }

        public Guid UnitId { get; private set; }

        public int Power { get; private set; }
        public bool HasResistToSlowdown { get; private set; }
        public bool HasResistToStandUp { get; private set; }
        public bool HasResistToStun { get; private set; }
        public int Mana { get; protected set; }
        public bool IsFacingRight { get; set; }

        public int MentalPower { get; private set; }

        public float MovementSpeed {get; private set;}

        public int Health { get; private set; }

        public int MaxHealth { get; private set; }

        public virtual void ChangeStat(StatAttribute statAttribute, int amount)
        {
            switch (statAttribute)
            {
                case StatAttribute.Health:
                    ChangeHealth(amount);
                    break;

                case StatAttribute.Mana:
                    amount = Mathf.Clamp(Mana + amount, 0, 100);
                    if (Mana != amount)
                    {
                        Mana = amount;
                        OnManaChanged(amount);
                    }

                    break;

                case StatAttribute.Power:
                    Power = Mathf.Clamp(Power + amount, 0, 9999);
                    break;

                case StatAttribute.DefensePercent:
                    _defensePercent = Mathf.Clamp(_defensePercent + amount, 0, 100);
                    break;

                case StatAttribute.MentalPower:
                    MentalPower = Mathf.Clamp(MentalPower + amount, 0, 100);
                    break;

                case StatAttribute.MoveSpeed:
                    MovementSpeed = Mathf.Clamp(MovementSpeed + amount, 0, 100);
                    break;
            }
        }

        public virtual void ChangeStat(StatAttribute statAttribute, float amount)
        {
            switch (statAttribute)
            {
                case StatAttribute.MoveSpeed:
                    MovementSpeed = Mathf.Clamp(MovementSpeed + amount, 0, 100);
                    break;
            }
        }

        public void IncreaseStatByPercent(StatAttribute statAttribute, int percent)
        {
            ChangeStatByPercent(statAttribute, percent, true);
        }

        public void DecreaseStatByPercent(StatAttribute statAttribute, int percent)
        {
            ChangeStatByPercent(statAttribute, percent, false);
        }

        public void Kill()
        {
            ChangeStat(StatAttribute.Health, -999999);
        }

        protected virtual void OnHealthChanged(int amount)
        {
            var handler = HealthChanged;
            if (handler != null)
            {
                handler(this, amount);
            }
        }

        protected virtual void OnManaChanged(int amount)
        {
            var handler = ManaChanged;
            if (handler != null)
            {
                handler(this, amount);
            }
        }

        protected virtual void OnDied()
        {
            var handler = Died;
            if (handler != null)
            {
                handler(this);
            }
        }

        private void ChangeStatByPercent(StatAttribute statAttribute, int percent, bool isIncrease)
        {
            percent = Mathf.Clamp(percent, 0, 100);

            if (statAttribute == StatAttribute.MoveSpeed)
            {
                var calculatePercentResultFloat = ValueUtility.CalculatePercent(
                    MovementSpeed, percent);
                ChangeStat(
                    statAttribute, 
                    isIncrease ? calculatePercentResultFloat : calculatePercentResultFloat * -1);
                return;
            }

            var baseValue = 0;
            switch (statAttribute)
            {
                case StatAttribute.Health:
                    baseValue = MaxHealth;
                    break;

                case StatAttribute.Power:
                    baseValue = _basePower;
                    break;

                case StatAttribute.DefensePercent:
                    baseValue = _baseDefensePercent;
                    break;

                case StatAttribute.MentalPower:
                    baseValue = _baseMentalPower;
                    break;
            }

            var calculatePercentResult = ValueUtility.CalculatePercent(baseValue, percent);
            calculatePercentResult = isIncrease ? calculatePercentResult : calculatePercentResult * -1;
            ChangeStat(statAttribute, calculatePercentResult);
        }

        private void ChangeHealth(int amount)
        {
            if (amount < 0 && _defensePercent > 0)
            {
                var affectedByDefence = ValueUtility.CalculatePercent(Mathf.Abs(amount), _defensePercent);
                amount = Mathf.Clamp(amount + affectedByDefence, amount, 0);
            }

            if (amount < 0)
            {
                amount = _absorbingBarrierController.Absorb(amount);
            }

            if (Health != amount)
            {
                Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
                
                OnHealthChanged(amount);
                
                if (Health == 0)
                {
                    OnDied();
                }
            }
        }

        public override bool Equals (object other)
        {
            if ( other == null )
            {
                return false;
            }

            if ( this.GetType ( ) != other.GetType ( ) )
            {
                return false;
            }
            return Equals ((Characteristics)other);
        }

        public override int GetHashCode()
        {
            return UnitId.GetHashCode();
        }

        private bool Equals(Characteristics other)
        {
            if ( other == null )
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return UnitId == other.UnitId;
        }

        public virtual void Dispose()
        {
        }

        protected virtual void OnTagChanged(Tag oldTag)
        {
            var handler = TagChanged;
            if (handler != null) handler(oldTag);
        }
    }
}
