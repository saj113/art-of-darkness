using System;
using System.Collections.Generic;
using System.Linq;
using Skills;
using Stats;
using UnitControllers;
using UnitControllers.UnitGameObjectBehavior;
using UnityEngine;
using Utilities;
using Object = System.Object;

namespace Core.Provider
{
    public class UnitProvider : IUnitRegistry, ITargetUnitProvider
    {
        private readonly IList<IStats> _units = new List<IStats>();
        private readonly IDictionary<Tag, IList<IStats>> _unitsByTag;

        public UnitProvider()
        {
            _unitsByTag = new Dictionary<Tag, IList<IStats>>
            {
                    {Tag.Player, new List<IStats>()},
                    {Tag.Ally, new List<IStats>()},
                    {Tag.Dead, new List<IStats>()},
                    {Tag.Enemy, new List<IStats>()},
                    {Tag.Default, new List<IStats>()},
            };
        }

        public IEnumerable<IStats> GetAll() { return _units; }
        public IEnumerable<IStats> Get(Tag tag) { return _unitsByTag[tag]; }

        public IStats Get(Tag tag, Vector2 position)
        {
            if (tag == Tag.Player)
            {
                return GetPlayer();
            }
            
            var unitByPosition =
                    Get(tag).FirstOrDefault(p => object.Equals(position, p.GameObjectController.Position));

            Contract.Ensure(unitByPosition != null,
                            string.Format("Unit with Tag {0} is not found by position {1}.", tag, position));

            return unitByPosition;
        }

        public IEnumerable<IStats> Get(Tag finder, TargetUnitRelation relation)
        {
            if (finder == Tag.Enemy && relation == TargetUnitRelation.Enemy)
            {
                return _unitsByTag[Tag.Player].Union(_unitsByTag[Tag.Ally]);
            }

            if ((finder == Tag.Ally || finder == Tag.Player) && relation == TargetUnitRelation.Friendly)
            {
                return _unitsByTag[Tag.Player].Union(_unitsByTag[Tag.Ally]);
            }

            switch (relation)
            {
                case TargetUnitRelation.Enemy:      return _unitsByTag[Tag.Enemy];
                case TargetUnitRelation.DeadOnly:   return _unitsByTag[Tag.Dead];
                case TargetUnitRelation.PlayerOnly: return _unitsByTag[Tag.Player];
            }

            throw new NotSupportedException();
        }

        public IEnumerable<IStats> Get(ICharacteristics finder,
                                       TargetUnitRelation relation,
                                       Bounds startPoint,
                                       Direction direction,
                                       float distance)
        {
            return Get(finder.Tag,
                       relation,
                       startPoint,
                       ValueUtility.GetDirection(finder, direction),
                       distance);
        }

        public IEnumerable<IStats> Get(Tag finder,
                                       TargetUnitRelation relation,
                                       Bounds startPoint,
                                       int direction,
                                       float distance)
        {
            var unitsByPosition = GetByDirection(finder, relation, startPoint.center.x, direction)
                                  .Select(p => new
                                  {
                                          Distance = p.GameObjectController.GetDistanceTo(startPoint),
                                          Unit = p
                                  })
                                  .Where(p => p.Distance <= distance)
                                  .OrderBy(p => p.Distance)
                                  .Select(p => p.Unit);

            return unitsByPosition;
        }

        public IEnumerable<IStats> Get(Tag finder, TargetUnitRelation relation, float startPoint, int direction, float distance)
        {
            var unitsByPosition = GetByDirection(finder, relation, startPoint, direction)
                .Select(p => new
                {
                    Distance = p.GameObjectController.GetDistanceTo(startPoint),
                    Unit = p
                })
                .Where(p => p.Distance <= distance)
                .OrderBy(p => p.Distance)
                .Select(p => p.Unit);

            return unitsByPosition;
        }

        public void Add(IStats unit)
        {
            _units.Add(unit);
            _unitsByTag[unit.Characteristics.Tag].Add(unit);
        }

        public void Remove(IStats unit)
        {
            _units.Remove(unit);
            _unitsByTag[unit.Characteristics.Tag].Remove(unit);
        }

        public void UpdateByTag(IStats unit, Tag oldTag)
        {
            _unitsByTag[oldTag].Remove(unit);
            _unitsByTag[unit.Characteristics.Tag].Add(unit);
        }

        private bool CheckDirection(float startPoint, Vector2 endPoint, int direction)
        {
            if (direction == 0) return true;
            if (direction > 0) return startPoint < endPoint.x;
            
            return startPoint > endPoint.x;
        }

        private IStats GetPlayer()
        {
            return _unitsByTag[Tag.Player].Single();
        }
        
        private IEnumerable<IStats> GetByDirection(
            Tag finder,
            TargetUnitRelation relation,
            float startPoint,
            int direction)
        {
            var unitsByRelation = Get(finder, relation);
            return unitsByRelation
                .Where(
                    p => CheckDirection(startPoint, p.GameObjectController.Position, direction));
        }
    }
}
