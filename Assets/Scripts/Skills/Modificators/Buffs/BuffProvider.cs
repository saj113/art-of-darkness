using System;
using Skills.Parameters.ModificatorParameters;
using Stats;

namespace Skills.Modificators.Buffs
{
    public static class BuffProvider
    {
        public static IBuff Create<T>(ISkillCaster caster, IStats targetStats, T parameters)
            where T : IBuffParameters
        {
            switch (parameters.Type)
            {
                case BuffType.Enslave:
                    return new EnslaveBuff(caster, targetStats, parameters as IEnslaveBuffParameters);
                case BuffType.Fear:
                    return new FearBuff(caster, targetStats, parameters as IFearBuffParameters);
                case BuffType.Stun:
                    return new StunBuff(caster, targetStats, parameters as IStunBuffParameters);
                case BuffType.Slowdown:
                    return new SlowdownBuff(caster, targetStats, parameters as ISlowdownBuffParameters);
                case BuffType.AbsorbShield:
                    return new AbsorbShieldBuff(caster, targetStats, parameters as IAbsorbShieldBuffParameters);
                case BuffType.ChangeStats:
                    return new ChangeStatsBuff(caster, targetStats, parameters as IChangeStatsBuffParameters);
                case BuffType.ChangeTag:
                    return new ChangeTagBuff(caster, targetStats, parameters as IChangeTagBuffParameters);
                case BuffType.DoT:
                    return new DoTBuff(caster, targetStats, parameters as IDoTBuffParameters);
                case BuffType.HoT:
                    return new HoTBuff(caster, targetStats, parameters as IRegenBuffParameters);
                case BuffType.MoT:
                    return new MoTBuff(caster, targetStats, parameters as IRegenBuffParameters);
            }
            
            throw new NotSupportedException(parameters.Type.ToString());
        }
    }
}