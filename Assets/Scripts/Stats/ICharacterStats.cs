using Skills.Weapons;

namespace Stats
{
    public interface ICharacterStats : IStats
    {
        ICharacterWeapon CharacterWeapon { get; }
    }
}
