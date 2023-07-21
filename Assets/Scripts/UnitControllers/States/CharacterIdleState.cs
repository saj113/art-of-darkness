using System.Collections.Generic;
using Core.Animation;
using GUIScripts.Messengers;
using Skills;
using Skills.Weapons;
using UnitControllers.TouchControllers;
using UnityEngine;

namespace UnitControllers.States
{
    internal class CharacterIdleState : State
    {
        private readonly ITouchController _touchController;
        private readonly IStateAnimationController _animationController;
        private readonly IStateController _stateController;
        private readonly ICharacterWeapon _characterWeapon;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly ICharacterMovementInfo _characterMovementInfo;
        private readonly ISkillUseFailedMessenger _skillUseFailedMessenger;

        private readonly IDictionary<string, ShapeType> _shapeTypesByKeys;

        public CharacterIdleState(
            IStateAnimationController animationController, 
            IStateController stateController, 
            ICharacterWeapon characterWeapon, 
            IUnitGameObjectController unitGameObjectController, 
            ITouchController touchController,
            ICharacterMovementInfo characterMovementInfo,
            ISkillUseFailedMessenger skillUseFailedMessenger)
        {
            _animationController = animationController;
            _stateController = stateController;
            _characterWeapon = characterWeapon;
            _unitGameObjectController = unitGameObjectController;
            _touchController = touchController;
            _characterMovementInfo = characterMovementInfo;
            _skillUseFailedMessenger = skillUseFailedMessenger;

            var shapeKeys = new[] { "q", "w", "e", "r", "t" };
            _shapeTypesByKeys = new Dictionary<string, ShapeType>();
            for (var i = 0; i < _characterWeapon.ShapeSkillParameters.Length; i++)
            {
                _shapeTypesByKeys.Add(
                        shapeKeys[i],
                        _characterWeapon.ShapeSkillParameters[i]
                                        .General.ShapeType);
            }
            
        }

        public override StateType Type
        {
            get { return StateType.CharacterIdleState; }
        }

        public override void EnableState()
        {
            _animationController.SetIdleAnimation();
            _touchController.Enable();
            _characterWeapon.SkillAnimationStarted += OnSkillAnimationStarted;
        }

        public override void ResetState()
        {
            _touchController.Disable();
            _characterMovementInfo.Dispose();
            _characterWeapon.SkillAnimationStarted -= OnSkillAnimationStarted;
        }

        public override void Update()
        {
            if (_characterMovementInfo.IsMoving())
            {
                _stateController.SetMoveState();
                return;
            }

            var shapeType = _touchController.TryRecognize();
            if (shapeType != ShapeType.None)
            {
                var position = _touchController.GetLastCenterPoint();
                _unitGameObjectController.TurnUnit(position);
                var failedReason = _characterWeapon.UseShapeSkill(shapeType, position);
                if (failedReason != SkillUseFailedReason.None)
                {
                    _skillUseFailedMessenger.ShowMessage(failedReason);
                }
            }
            else
            {
                foreach (var key in _shapeTypesByKeys.Keys)
                {
                    if (Input.GetKeyDown(key))
                    {
                        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        _unitGameObjectController.TurnUnit(position.x);
                        var failedReason = _characterWeapon.UseShapeSkill(_shapeTypesByKeys[key], position.x);
                        if (failedReason != SkillUseFailedReason.None)
                        {
                            _skillUseFailedMessenger.ShowMessage(failedReason);
                        }
                        
                        break;
                    }
                }
            }
        }
        
        private void OnSkillAnimationStarted()
        {
            _stateController.SetAttackState();
        }
    }
}
