using System;
using System.Collections.Generic;
using Skills;

namespace GUIScripts
{
    public static class TextProvider
    {
        private static readonly IDictionary<TalentNameKey, string> TalentNames =
            new Dictionary<TalentNameKey, string>();
        private static readonly IDictionary<TalentNameKey, string> TalentDescription =
            new Dictionary<TalentNameKey, string>();
        private static readonly IDictionary<SkillUseFailedReason, string> SkillUseFailMessages = 
            new Dictionary<SkillUseFailedReason, string>();

        static TextProvider()
        {
            InitializeTalentsDictionaries(Language.RU);
            InitializeSkillUseFailMessages(Language.EN);
        }

        public static string GetTalentName(TalentNameKey key)
        {
            return TalentNames[key];
        }

        public static string GetTalentDescription(TalentNameKey key)
        {
            return TalentDescription[key];
        }

        public static string GetSkillCooldownFailedMessage(SkillUseFailedReason skillUseFailedReason)
        {
            return SkillUseFailMessages[skillUseFailedReason];
        }
        
        private static void InitializeTalentsDictionaries(Language language)
        {
            switch (language)
            {
                case Language.RU:
                    TalentNames.Add(TalentNameKey.Magicka_ShadowBolts, TextLibrary.Talents.RU_Magicka_ShadowBoltsName);
                    TalentNames.Add(TalentNameKey.Magicka_NecromanticSword, TextLibrary.Talents.RU_Magicka_NecromanticSwordName);
                    TalentNames.Add(TalentNameKey.Magicka_BoneSpear, TextLibrary.Talents.RU_Magicka_BoneSpearName);
                    TalentNames.Add(TalentNameKey.Magicka_Enslave, TextLibrary.Talents.RU_Magicka_EnslaveName);
                    TalentNames.Add(TalentNameKey.Magicka_NecroticShield, TextLibrary.Talents.RU_Magicka_NecroticShieldName);
                    TalentNames.Add(TalentNameKey.Magicka_ShadowJump, TextLibrary.Talents.RU_Magicka_ShadowJumpName);
                    break;
                
                default:
                    throw new NotSupportedException(language.ToString());
            }
        }

        private static void InitializeSkillUseFailMessages(Language language)
        {
            switch (language)
            {
                case Language.EN:
                    SkillUseFailMessages.Add(SkillUseFailedReason.HealthNotEnough, TextLibrary.SkillUseFail.EN_HealthNotEnough);
                    SkillUseFailMessages.Add(SkillUseFailedReason.ManaNotEnough, TextLibrary.SkillUseFail.EN_ManaNotEnough);
                    SkillUseFailMessages.Add(SkillUseFailedReason.TimeCooldown, TextLibrary.SkillUseFail.EN_NotReady);
                    SkillUseFailMessages.Add(SkillUseFailedReason.None, TextLibrary.SkillUseFail.EN_NotReady);
                    SkillUseFailMessages.Add(SkillUseFailedReason.CorpsesNotEnoughNearby, TextLibrary.SkillUseFail.EN_CorpsesNotEnoughNearby);
                    SkillUseFailMessages.Add(SkillUseFailedReason.CorpsesNotEnoughInPlace, TextLibrary.SkillUseFail.EN_CorpsesNotEnoughInPlace);
                    SkillUseFailMessages.Add(SkillUseFailedReason.TargetsNotFound, TextLibrary.SkillUseFail.EN_TargetsNotFound);
                    SkillUseFailMessages.Add(SkillUseFailedReason.ShapeNotRecognized, TextLibrary.SkillUseFail.EN_ShapeNotRecognized);
                    SkillUseFailMessages.Add(SkillUseFailedReason.StateIsInvalid, TextLibrary.SkillUseFail.EN_StateIsInvalid);
                    break;
                case Language.RU:
                    SkillUseFailMessages.Add(SkillUseFailedReason.HealthNotEnough, TextLibrary.SkillUseFail.RU_HealthNotEnough);
                    SkillUseFailMessages.Add(SkillUseFailedReason.ManaNotEnough, TextLibrary.SkillUseFail.RU_ManaNotEnough);
                    SkillUseFailMessages.Add(SkillUseFailedReason.TimeCooldown, TextLibrary.SkillUseFail.RU_NotReady);
                    SkillUseFailMessages.Add(SkillUseFailedReason.None, TextLibrary.SkillUseFail.RU_NotReady);
                    SkillUseFailMessages.Add(SkillUseFailedReason.CorpsesNotEnoughNearby, TextLibrary.SkillUseFail.RU_CorpsesNotEnoughNearby);
                    SkillUseFailMessages.Add(SkillUseFailedReason.CorpsesNotEnoughInPlace, TextLibrary.SkillUseFail.RU_CorpsesNotEnoughInPlace);
                    SkillUseFailMessages.Add(SkillUseFailedReason.TargetsNotFound, TextLibrary.SkillUseFail.RU_TargetsNotFound);
                    SkillUseFailMessages.Add(SkillUseFailedReason.ShapeNotRecognized, TextLibrary.SkillUseFail.RU_ShapeNotRecognized);
                    SkillUseFailMessages.Add(SkillUseFailedReason.StateIsInvalid, TextLibrary.SkillUseFail.RU_StateIsInvalid);
                    break;
                
                default:
                    throw new NotSupportedException(language.ToString());
            }
        }
    }
}
