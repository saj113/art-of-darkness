using System;

namespace UnitControllers.AbsorbingBarrierBehavior
{
    public class AbsorbingBarrier
    {
        public event Action<int> Absorbed;
        public event Action BarrierElapsed;

        public AbsorbingBarrier(int power)
        {
            Power = power;
        }

        public int Power { get; private set; }

        public int Absorb(int amount)
        {
            if (Power > 0 && amount > 0)
            {
                if (amount > Power)
                {
                    amount -= Power;
                    OnAbsorbed(Power);
                    Power = 0;
                    OnBarrierElapsed();
                }
                else
                {
                    Power -= amount;
                    OnAbsorbed(amount);
                    amount = 0;
                }
            }

            return amount;
        }

        protected virtual void OnAbsorbed(int amount)
        {
            var handler = Absorbed;
            if (handler != null) handler(amount);
        }

        protected virtual void OnBarrierElapsed()
        {
            var handler = BarrierElapsed;
            if (handler != null) handler();
        }
    }
}
