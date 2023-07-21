using Core.Prefabs;
using Skills;

namespace GUIScripts.Menu.Talent
{
    public class TalentInfoCollectionProvider
    {
        private readonly ICharacterSkillPrefabProvider _prefabProvider;
        public TalentInfoCollectionProvider() { _prefabProvider = new CharacterSkillPrefabProvider(); }
        public ITalentInfo[] GetMeleeTalentInfoCollection()
        {
            var necromancerSword = new SkillInfo(
                TalentNameKey.Magicka_NecromanticSword,
                _prefabProvider.GetNecromancerSwordSprite(),
                new TalentStatus(false, "test"));
            return new[]
            {
                new TalentInfo(TalentNameKey.Magicka_NecromanticSword,
                    _prefabProvider.GetNecromancerSwordSprite(),
                    new TalentStatus(false, "test"),
                    necromancerSword,
                    necromancerSword,
                    necromancerSword,
                    necromancerSword,
                    necromancerSword)
            };
        }

        public static ITalentInfo[] GetRangeTalentInfoCollection()
        {
            return null;
        }
    }
}