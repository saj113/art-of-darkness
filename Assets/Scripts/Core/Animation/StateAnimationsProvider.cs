using System;
using Stats;

namespace Core.Animation
{
    public static class StateAnimationsProvider
    {
        public static IStateAnimations GetByTemplate(StateAnimationTemplate template)
        {
            switch (template)
            {
                case StateAnimationTemplate.Necromancer:
                    return GetNecromancerAnimations();
                
                case StateAnimationTemplate.SoldierSword:
                    return GetSoldierSwordAnimations();
                
                case StateAnimationTemplate.SoldierShield:
                    return GetSoldierShieldAnimations();
                
                case StateAnimationTemplate.SoldierDual:
                    return GetSoldierDualAnimations();
                
                case StateAnimationTemplate.Zombie:
                    return GetZombieAnimations();
                
                case StateAnimationTemplate.Mage:
                    return GetMageAnimations();
                
                case StateAnimationTemplate.Dog:
                    return GetDogAnimations();
                
                case StateAnimationTemplate.Ghost:
                    return GetGhostAnimations();
            }
            
            throw new NotSupportedException();
        }

        private static IStateAnimations GetNecromancerAnimations()
        {
            return new StateAnimations(
                AnimationConstants.State.Character.Idle,
                AnimationConstants.State.Character.Fall,
                AnimationConstants.State.Character.Run,
                AnimationConstants.State.Character.Stun,
                null,
                0.7f);
        }
        
        private static IStateAnimations GetSoldierSwordAnimations()
        {
            return new StateAnimations(
                AnimationConstants.State.Human.IdleSoldier,
                AnimationConstants.State.Human.Fall,
                AnimationConstants.State.Human.RunWithSword,
                AnimationConstants.State.Human.Stun,
                AnimationConstants.State.Human.StandUp);
        }
        
        private static IStateAnimations GetSoldierDualAnimations()
        {
            return new StateAnimations(
                AnimationConstants.State.Human.IdleSoldier,
                AnimationConstants.State.Human.Fall,
                AnimationConstants.State.Human.RunWithDual,
                AnimationConstants.State.Human.Stun,
                AnimationConstants.State.Human.StandUp);
        }
        
        private static IStateAnimations GetSoldierShieldAnimations()
        {
            return new StateAnimations(
                AnimationConstants.State.Human.IdleSoldier,
                AnimationConstants.State.Human.Fall,
                AnimationConstants.State.Human.RunWithShield,
                AnimationConstants.State.Human.Stun,
                AnimationConstants.State.Human.StandUp);
        }
        
        private static IStateAnimations GetZombieAnimations()
        {
            return new StateAnimations(
                AnimationConstants.State.Human.IdleZombie,
                AnimationConstants.State.Human.FallZombie,
                AnimationConstants.State.Human.RunZombie,
                AnimationConstants.State.Human.StunZombie,
                AnimationConstants.State.Human.StandUpZombie);
        }
        
        private static IStateAnimations GetMageAnimations()
        {
            return new StateAnimations(
                AnimationConstants.State.Human.IdleMage,
                AnimationConstants.State.Human.Fall,
                AnimationConstants.State.Human.RunMage,
                AnimationConstants.State.Human.Stun,
                AnimationConstants.State.Human.StandUp);
        }
        
        private static IStateAnimations GetDogAnimations()
        {
            return new StateAnimations(
                AnimationConstants.State.Dog.Idle,
                AnimationConstants.State.Dog.Fall,
                AnimationConstants.State.Dog.Run,
                AnimationConstants.State.Dog.Stun,
                AnimationConstants.State.Dog.StandUp);
        }
        
        private static IStateAnimations GetGhostAnimations()
        {
            return new StateAnimations(
                AnimationConstants.State.Ghost.Idle,
                AnimationConstants.State.Ghost.Fall,
                AnimationConstants.State.Ghost.Run,
                null,
                null);
        }
    }
}
