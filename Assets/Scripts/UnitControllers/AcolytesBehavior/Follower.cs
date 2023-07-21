using System;
using System.Collections.Generic;
using Stats;
using Utilities;

namespace UnitControllers.AcolytesBehavior
{
    public class Follower : IFollower
    {
        private readonly ICharacteristics _characteristics;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IList<Guid> _units = new List<Guid>(); 

        public Follower(ICharacteristics characteristics, IUnitGameObjectController unitGameObjectController)
        {
            _characteristics = characteristics;
            _unitGameObjectController = unitGameObjectController;
        }

        public bool IsFolowerValid()
        {
            return _characteristics.Health > 0;
        }

        public void GetFollowBounds(Guid unitId, out float from, out float to)
        {
            from = 0;
            to = 0;

            var index = _units.IndexOf(unitId);
            if (index % 2 == 0)
            {
                to = _unitGameObjectController.Position.x - Constants.AcolyteToCasterDistance;
                from = to - Constants.AcolyteToAcolyteDistance * _units.Count;
            }
            else
            {
                from = _unitGameObjectController.Position.x + Constants.AcolyteToCasterDistance;
                to = from + Constants.AcolyteToAcolyteDistance * _units.Count;
            }
        }

        public void AddAcolyte(Guid id)
        {
            _units.Add(id);
        }

        public void RemoveAcolyte(Guid id)
        {
            _units.Remove(id);
        }
    }
}