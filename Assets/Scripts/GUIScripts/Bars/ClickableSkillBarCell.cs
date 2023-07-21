using Core.UnityFramework;
using GUIScripts.Messengers;
using GUIScripts.Triggers;
using Skills;
using Skills.Parameters;
using Skills.Weapons;
using UnitControllers;
using UnitControllers.States;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUIScripts.Bars
{
    public class ClickableSkillBarCell : SkillBarCell<IExtendedSkillParameters>
    {
        private readonly ICharacterWeapon _weapon;
        private readonly IStateController _stateController;
        private readonly ISkillUseFailedMessenger _skillUseFailedMessenger;
        private readonly IClickableSkillTrigger _clickableSkillTrigger;

        private bool _isPressed;
        private float _pressTime;
        private bool _isHeld;
        private const float MinHoldTime = 0.3f;
        private readonly IUnityUpdateEvents _unityUpdateEvents;
        public ClickableSkillBarCell(
            IExtendedSkillParameters skillParameters,
            ICharacterWeapon characterWeapon,
            IStateController stateController,
            IClickableSkillTrigger clickableSkillTrigger,
            IUnityUpdateEvents unityUpdateEvents,
            ISkillUseFailedMessenger skillUseFailedMessenger)
            : base(skillParameters, clickableSkillTrigger, unityUpdateEvents)
        {
            _clickableSkillTrigger = clickableSkillTrigger;
            _weapon = characterWeapon;
            _stateController = stateController;
            _unityUpdateEvents = unityUpdateEvents;
            _unityUpdateEvents.FixedUpdateFired += FixedUpdateFired;
            _clickableSkillTrigger.PointerDown += HandlePointerDown;
            _clickableSkillTrigger.PointerUp += HandlePointerUp;
            _skillUseFailedMessenger = skillUseFailedMessenger;
        }

        private void HandlePointerUp()
        {
            _isHeld = false;
            _isPressed = false;

            if (_pressTime < MinHoldTime) 
            {
                if (IsStateFree())
                {
                    var failedReason = _weapon.UseSkill(SkillParameters);
                    if (failedReason != SkillUseFailedReason.None)
                    {
                        _skillUseFailedMessenger.ShowMessage(failedReason);
                    }
                }
            }
            else if (SkillParameters.HeldSkillParameters != null)
            {
                _weapon.StopSkill();
            }
        }

        private void HandlePointerDown()
        {
            _isPressed = true;
        }

        private void FixedUpdateFired(float deltaTime)
        {
            if (SkillParameters.HeldSkillParameters == null)
            {
                return;
            }

            _pressTime = _isPressed ? _pressTime + deltaTime : 0.0f;
            if (_pressTime > MinHoldTime && !_isHeld)
            {
                _isHeld = true;
                var failedReason = _weapon.UseSkill(SkillParameters.HeldSkillParameters);
                if (failedReason != SkillUseFailedReason.None)
                {
                    _skillUseFailedMessenger.ShowMessage(failedReason);
                }
            }
        }

        private bool IsStateFree()
        {
            return _stateController.CurrentStateType == StateType.UnitIdleState ||
                   _stateController.CurrentStateType == StateType.CharacterMoveState ||
                   _stateController.CurrentStateType == StateType.CharacterIdleState ||
                   _stateController.CurrentStateType == StateType.AttackState;
        }

        public override void Dispose()
        {
            base.Dispose();
            _unityUpdateEvents.FixedUpdateFired -= FixedUpdateFired;
            _clickableSkillTrigger.PointerDown -= HandlePointerDown;
            _clickableSkillTrigger.PointerUp -= HandlePointerUp;
        }
    }
}
