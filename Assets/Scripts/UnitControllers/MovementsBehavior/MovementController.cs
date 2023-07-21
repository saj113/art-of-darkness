using System;
using Core.Animation;
using Stats;
using UnityEngine;

namespace UnitControllers.MovementsBehavior
{
    internal class MovementController : IMovementController
    {
        private float _delay = 0.0f;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly ICharacteristics _characteristics;
        private readonly IStateAnimationController _animationController;
        private bool _isRunning;

        public MovementController(
            IUnitGameObjectController unitGameObjectController, 
            ICharacteristics characteristics,
            IStateAnimationController animationController)
        {
            _unitGameObjectController = unitGameObjectController;
            _characteristics = characteristics;
            _animationController = animationController;
        }

        public void MoveWithVelocity(int direction)
        {
            try
            {
                _unitGameObjectController.Move(
                    _characteristics.MovementSpeed * Time.deltaTime, 
                    direction);
                MoveToDirection(direction);
            }
            catch (Exception ex)
            {
                Debug.LogError("MoveCommand error: " + ex.Message);
            }
        }

        public void MoveToTargetWithDelay(float targetXPosition)
        {
            if (_delay < 0.3)
            {
                _delay += Time.deltaTime;
                return;
            }

            var direction = _unitGameObjectController.Position.x < targetXPosition ? 1 : -1;
            MoveWithVelocity(direction);
        }
        
        public void Stop()
        {
            if(_animationController.IsAnimationRun())
            {
                _animationController.SetIdleAnimation();
            }

            _delay = 0.0f;
            _isRunning = false;
        }

        private void MoveToDirection(int direction)
        {
            if (!_isRunning)
            {
                _animationController.SetRunAnimation();
                _isRunning = true;
            }
            _unitGameObjectController.TurnUnit(direction);
        }
    }
}
