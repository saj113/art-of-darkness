using System.Collections.Generic;
using System.Linq;
using Stats;

namespace UnitControllers.AcolytesBehavior
{
    internal class AcolyteController : IAcolyteController
    {
        private readonly IFollower _follower;
        private readonly IDictionary<AcolyteType, HashSet<ICharacteristics>> _acolytesRegistered = new Dictionary<AcolyteType, HashSet<ICharacteristics>>();

        public AcolyteController(IFollower follower)
        {
            _follower = follower;
            _acolytesRegistered[AcolyteType.Summon] = new HashSet<ICharacteristics>();
            _acolytesRegistered[AcolyteType.Zombie] = new HashSet<ICharacteristics>();
        }

        public void AddAcolyte(IUnitStats unitStats, AcolyteType acolyteType)
        {
            if (MaximumAcolytesReached(acolyteType))
            {
                var first = _acolytesRegistered[acolyteType].FirstOrDefault();
                first.Died -= OnAcolyteIsDead;
                _acolytesRegistered[acolyteType].Remove(first);
                first.Kill();
            }

            _follower.AddAcolyte(unitStats.Characteristics.UnitId);
            unitStats.Characteristics.Died += OnAcolyteIsDead;
            unitStats.FollowingController.FollowingInstructions = _follower;

            _acolytesRegistered[acolyteType].Add(unitStats.Characteristics);
        }

        private bool MaximumAcolytesReached(AcolyteType acolyteType)
        {
            var maxAcolytes = acolyteType == AcolyteType.Zombie ? 5 : 10;
            return _acolytesRegistered[acolyteType].Count >= maxAcolytes;
        }

        private void OnAcolyteIsDead(ICharacteristics characteristics)
        {
            characteristics.Died -= OnAcolyteIsDead;
            _follower.RemoveAcolyte(characteristics.UnitId);
            
            foreach(var unitTags in _acolytesRegistered.Values)
            {
                unitTags.Remove(characteristics);
            }
        }

        public void Dispose()
        {
            _acolytesRegistered[AcolyteType.Summon].ToList().ForEach(p => p.Died -= OnAcolyteIsDead);
            _acolytesRegistered[AcolyteType.Zombie].ToList().ForEach(p => p.Died -= OnAcolyteIsDead);
        }
    }
}
