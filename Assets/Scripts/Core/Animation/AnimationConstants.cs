namespace Core.Animation
{
    public static class AnimationConstants
    {
        public static class Skill
        {
            public static class Character
            {
                public const string AttackSword1 = "attackSword1";
                public const string AttackSword2 = "attackSword2";
                public const string AttackSword3 = "attackSword3";
                public const string Summon = "spellSummon";
                public const string Summon2 = "spellSummon2";
                public const string SummonAndStaffForward = "spellSummonAndStaffForward";
                public const string StaffUp = "spellStaffUp";
                public const string MaintainingHandForward = "maintainingSpellHandForward";
                public const string MaintainingStaffForward = "maintainingSpellStaffForward";
                public const string MaintainingStaffAndHandForward = "maintainingSpellStaffAndHandForward";
            }
        }
        
        public static class State
        {
            public static class Character
            {
                public const string Idle = "stateIdle";
                public const string Fall = "stateFall";
                public const string Run = "stateRun";
                public const string Stun = "stateStun";
            }
            
            public static class Human
            {
                public const string IdleSoldier = "stateIdleSoldier";
                public const string IdleZombie = "stateIdleZombie";
                public const string IdleMage = "stateIdleMage";
                public const string RunMage = "stateRunMage";
                public const string RunZombie = "stateRunZombie";
                public const string RunWithSword = "stateRunWithSword";
                public const string RunWithDual = "stateRunWithDual";
                public const string RunWithShield = "stateRunWithShield";
                public const string FallZombie = "stateFallZombie";
                public const string StandUpZombie = "stateStandUpZombie";
                public const string StunZombie = "stateStunZombie";
                public const string Fall = "stateFall";
                public const string StandUp = "stateStandUp";
                public const string Stun = "stateStun";
            }
            
            public static class Dog
            {
                public const string Idle = "stateIdle";
                public const string Fall = "stateFall";
                public const string Run  = "stateRun";
                public const string Stun = "stateStun";
                public const string StandUp = "stateStandUp";
            }
            
            public static class Ghost
            {
                public const string Idle = "stateIdle";
                public const string Fall = "stateFall";
                public const string Run  = "stateRun";
            }
        }
    }
}
