using Core.Animation;
using Stats;
using UnityEngine;

namespace UnitControllers.States
{
    internal class UnitDeadState : State
    {
        private float _deadElapsedTime;

        private readonly ICharacteristics _characteristics;
        private readonly IUnitGameObjectController _gameObjectController;
        private readonly IStateAnimationController _stateAnimationController;

        public UnitDeadState(
            ICharacteristics characteristics, 
            IUnitGameObjectController gameObjectController,
            IStateAnimationController stateAnimationController)
        {
            _characteristics = characteristics;
            _gameObjectController = gameObjectController;
            _stateAnimationController = stateAnimationController;
        }

        public override StateType Type
        {
            get { return StateType.UnitDeadState; }
        }

        public override void EnableState()
        {
            _characteristics.Tag = Tag.Dead;
            _stateAnimationController.SetDeadAnimation();
            _deadElapsedTime = _characteristics.HasResistToStandUp ? 1 : 60.0f;
        }

        public override void Update()
        {
            _deadElapsedTime -= Time.deltaTime;
            if (_deadElapsedTime <= 0.0f)
            {
                _gameObjectController.Destroy();
            }
        }
    }
}
