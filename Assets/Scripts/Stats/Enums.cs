namespace Stats
{
    public enum StatAttribute { 
        MoveSpeed, 
        Power, 
        Health, 
        DefensePercent, 
        MentalPower, 
        Mana,
        DeathTime
    }

    public enum Tag { Default, Player, Ally, Enemy, Dead }
    public enum DetectionTargetBehavior
    {
        NearestTarget,
        AggressiveTarget,
        PlayerTarget
    }

    public enum StateAnimationTemplate
    {
        Necromancer,
        SoldierSword,
        SoldierDual,
        SoldierShield,
        Mage,
        Zombie,
        Ghost,
        Dog
    }

    public enum CharacteristicsTemplate
    {
        Necromancer,
        Summon,
        Dog,
        SimpleSoldier,
        AdvancedSoldier,
        AdvancedShieldSoldier,
        SimpleMage,
        AdvancedMage
    }
}