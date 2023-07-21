using System;
using Skills.Cooldown;

namespace Skills
{
    public static class SkillExtensions
    {
        public static bool AreReady(this ISkillCooldown[] skillCooldownCollection, out SkillUseFailedReason skillUseFailedReason)
        {
            foreach (var skillCooldown in skillCooldownCollection)
            {
                if (!skillCooldown.IsReady())
                {
                    switch (skillCooldown.Type)
                    {
                        case SkillCooldownType.Health:
                            skillUseFailedReason = SkillUseFailedReason.HealthNotEnough;
                            break;
                        case SkillCooldownType.Mana:
                            skillUseFailedReason = SkillUseFailedReason.ManaNotEnough;
                            break;
                        case SkillCooldownType.Time:
                            skillUseFailedReason = SkillUseFailedReason.TimeCooldown;
                            break;
                        default:
                            throw new NotSupportedException(skillCooldown.Type.ToString());
                    }
                    
                    return false;
                }
            }

            skillUseFailedReason = SkillUseFailedReason.None;
            return true;
        }
        
        public static void GiveCost(this ISkillCooldown[] skillCooldownCollection)
        {
            foreach (var skillCooldown in skillCooldownCollection)
            {
                skillCooldown.GiveCost();
            }
        }
    }
}