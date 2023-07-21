using System;
using Core;
using Level;
using Stats;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;

namespace UnitControllers.UnitGameObjectBehavior
{
    internal class UnitGameObjectController : IUnitGameObjectController
    {
        private readonly GameObject _gameObject;
        private readonly ICharacteristics _characteristics;
        private readonly Collider2D _collider;
        private readonly ILevelPreferences _levelPreferences;
        
        public event Action Destroyed;

        public UnitGameObjectController(
            ICharacteristics characteristics, 
            GameObject gameObject, 
            ILevelPreferences levelPreferences)
        {
            _characteristics = characteristics;
            _gameObject = gameObject;
            _collider = gameObject.GetComponent<Collider2D>();
            _levelPreferences = levelPreferences.ThrowIfNull(nameof(levelPreferences));
        }

        public Vector2 CenterPosition
        {
            get
            {
                return new Vector2(_gameObject.transform.position.x, _collider.bounds.center.y);
            }
        }

        public Bounds Bounds
        {
            get { return _collider.bounds; }
        }

        public Vector2 Position
        {
            get { return _gameObject.transform.position; }
            set
            {
                _gameObject.transform.position = new Vector2(
                    Mathf.Clamp(value.x, _levelPreferences.LeftWall, _levelPreferences.RightWall),
                    value.y);
            }
        }

        public void Destroy()
        {
            var handler = Destroyed;

            if (handler != null)
            {
                handler();
            }

            Object.Destroy(_gameObject);
        }


        public void AddChild(Transform transform)
        {
            transform.SetParent(_gameObject.transform);
        }

        public void Move(float velocity, int direction)
        {
            var moveDistance = Vector2.MoveTowards(
                Position, 
                new Vector2(Position.x + 10 * direction, Position.y), 
                velocity);
            Position = moveDistance;
        }

        public void SetActive(bool isActive)
        {
            _gameObject.SetActive(isActive);
        }

        public void TurnUnit(int direction)
        {
            if ((direction > 0 && !_characteristics.IsFacingRight) ||
                (direction < 0 && _characteristics.IsFacingRight))
            {
                TurnUnit();
            }
        }

        public void TurnUnit(float to)
        {
            var directionToTarget = ValueUtility.GetDirection(CenterPosition.x, to);
            TurnUnit(directionToTarget);
        }
        
        private void TurnUnit()
        {
            _characteristics.IsFacingRight = !_characteristics.IsFacingRight;
            var theScale = _gameObject.transform.localScale;

            theScale.x *= -1;
            _gameObject.transform.localScale = theScale;
        }
    }
}
