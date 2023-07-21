using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Animation;
using Core.UnityFramework;
using Skills.Behaviors;
using Skills.Cooldown;
using Skills.Parameters;

namespace Skills.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private readonly ISkillParameters[] _skillParameters;
        public event Action SkillAnimationFinished;
        public event Action SkillAnimationInterrupted;
        public event Action SkillAnimationStarted;

        private IBehaviorActivatable _skillActivationProcessing;
        private ISkillParameters _skillParametersProcessing;
        
        protected Weapon(ISkillAnimationController skillAnimationController)
        {
            SkillAnimationController = skillAnimationController;
            SkillAnimationController.AnimationEvent += SkillAnimationControllerOnAnimationEvent;
            SkillAnimationController.AnimationComplete += SkillAnimationControllerOnAnimationComplete;
        }
        protected ISkillAnimationController SkillAnimationController { get; private set; }

        protected bool IsSkillActivationProcessing()
        {
            return _skillActivationProcessing != null;
        }

        protected void StartSkillActivation(
            IBehaviorActivatable behaviorActivatable, 
            ISkillParameters skillParameters)
        {
            _skillActivationProcessing = behaviorActivatable;
            _skillParametersProcessing = skillParameters;
            SkillAnimationController.StartSkillAnimation(skillParameters.Animation);
            OnSkillAnimationStarted();
        }

        private void SkillAnimationControllerOnAnimationComplete()
        {
            FinishActivation();
            OnSkillAnimationFinished();
        }

        private void SkillAnimationControllerOnAnimationEvent()
        {
            _skillActivationProcessing.Activate();
        }

        private void FinishActivation()
        {
            _skillActivationProcessing.FinishActivation();
            _skillActivationProcessing = null;
            _skillParametersProcessing.General.SkillCooldownCollection.GiveCost();
            _skillParametersProcessing = null;
        }

        protected virtual void OnSkillAnimationFinished()
        {
            var handler = SkillAnimationFinished;
            if (handler != null)
            {
                handler();
            }
        }

        protected virtual void OnSkillAnimationInterrupted()
        {
            var handler = SkillAnimationInterrupted;
            if (handler != null)
            {
                handler();
            }
        }

        protected virtual void OnSkillAnimationStarted()
        {
            var handler = SkillAnimationStarted;
            if (handler != null)
            {
                handler();
            }
        }

        public void Dispose()
        {
            SkillAnimationController.AnimationEvent -= SkillAnimationControllerOnAnimationEvent;
            SkillAnimationController.AnimationComplete -= SkillAnimationControllerOnAnimationComplete;
        }
    }
}
