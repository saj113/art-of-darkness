using System.Linq;
using Core;
using Core.Animation;
using Core.UnityFramework;
using Skills.Parameters;
using Stats;
using UnitControllers;
using UnitControllers.TouchControllers;
using UnityEngine;
using Utilities;

namespace Skills.Weapons
{
    public class CharacterWeapon : Weapon, ICharacterWeapon
    {
        private readonly ISkillBehaviorProvider _skillBehaviorProvider;
        private bool _isSkillExecuting;
        private ISkillParameters _nextSkill;
        private readonly ICharacterMovementInfo _characterMovementInfo;
        private readonly Core.ILogger _logger;
        public CharacterWeapon(
            ISkillBehaviorProvider skillBehaviorProvider,
            ISkillAnimationController skillAnimationController,
            IExtendedSkillParameters[] extendedSkillParameterses,
            ISkillParameters[] shapeSkillParameters,
            IExtendedSkillParameters[] sealSkillParameters,
            ICharacterMovementInfo characterMovementInfo,
            Core.ILogger logger)
            : base(skillAnimationController)
        {
            _logger = logger;
            _characterMovementInfo = characterMovementInfo;
            _skillBehaviorProvider = skillBehaviorProvider;
            ExtendedSkillParameters = extendedSkillParameterses;
            ShapeSkillParameters = shapeSkillParameters;
            SealSkillParameters = sealSkillParameters;
        }

        public IExtendedSkillParameters[] ExtendedSkillParameters { get; private set; }
        public ISkillParameters[] ShapeSkillParameters { get; private set; }
        
        public IExtendedSkillParameters[] SealSkillParameters { get; private set; }

        public SkillUseFailedReason UseShapeSkill(ShapeType shapeType, float position)
        {
            var shapeSkillParameters = ShapeSkillParameters.FirstOrDefault(p => p.General.ShapeType == shapeType);
            if (shapeSkillParameters == null)
            {
                return SkillUseFailedReason.ShapeNotRecognized;
            }
            
            if (IsSkillActivationProcessing())
            {
                _nextSkill = shapeSkillParameters;
                return SkillUseFailedReason.StateIsInvalid;
            }

            if (!shapeSkillParameters.General.SkillCooldownCollection.AreReady(out var skillNotReadyReason))
            {
                return skillNotReadyReason;
            }
            
            var behaviorToPositionActivatable =
                _skillBehaviorProvider.GetSkillBehaviorToPointComponent(
                    shapeSkillParameters.BehaviorParameters,
                    position);
            if (!behaviorToPositionActivatable.IsActivatable(out var skillNotActivatableReason))
            {
                return skillNotActivatableReason;
            }
            
            StartSkillActivation(
                behaviorToPositionActivatable, 
                shapeSkillParameters);
            return SkillUseFailedReason.None;
        }

        public SkillUseFailedReason UseSkill(ISkillParameters skillParameters)
        {
            if (IsSkillActivationProcessing())
            {
                _nextSkill = skillParameters;
                return SkillUseFailedReason.StateIsInvalid;
            }
            
            if (!skillParameters.General.SkillCooldownCollection.AreReady(out var skillNotReadyReason))
            {
                return skillNotReadyReason;
            }

            var behaviorActivatable = _skillBehaviorProvider.GetSkillBehavior(
                skillParameters.BehaviorParameters);
            if (!behaviorActivatable.IsActivatable(out var skillNotActivatableReason))
            {
                return skillNotActivatableReason;
            }
            
            StartSkillActivation(behaviorActivatable, skillParameters);
            return SkillUseFailedReason.None;
        }

        public void StopSkill()
        {
            _nextSkill = null;
            SkillAnimationController.StopSkillAnimation();
        }

        protected override void OnSkillAnimationFinished()
        {
            base.OnSkillAnimationFinished();
            var temp = _nextSkill;
            _nextSkill = null;
            if (temp == null || _characterMovementInfo.IsMoving() && UseSkill(temp) != SkillUseFailedReason.None)
            {
                base.OnSkillAnimationFinished();
            }
        }
    }
}
