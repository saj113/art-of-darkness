using Stats;

namespace Core
{
    public delegate void DamageInflictedAction(IStats affronter, int amountDamage);

    public static class EventAggregator
    {
        public static event DamageInflictedAction DamageInflicted;

        public static void OnDamageInflicted(IStats affronter, int amountdamage)
        {
            var handler = DamageInflicted;
            if (handler != null) handler(affronter, amountdamage);
        }
    }
}
