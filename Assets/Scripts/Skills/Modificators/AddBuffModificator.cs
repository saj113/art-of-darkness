using System;
using Skills.Modificators.Buffs;
using Skills.Parameters.ModificatorParameters;
using Stats;
using Utilities;

namespace Skills.Modificators
{
    public class AddBuffModificator<T> : Modificator
        where T : IBuffParameters
    {
        public AddBuffModificator(
            ISkillCaster caster,
            TargetUnitRelation targetUnitRelation,
            T buffParameters) 
            : base(caster, targetUnitRelation)
        {
            Parameters = buffParameters;
        }

        public override bool IsBuff 
        {
            get { return true; }
        }

        protected T Parameters { get; private set; }

        protected override void ApplyChanges(IStats target)
        {
            if (Parameters.ConsiderResist && !ConditionUtility.CheckResistance(
                Caster.Characteristics.MentalPower, target.Characteristics.MentalPower))
            {
                return;
            }

            if (Parameters.Chance > 0 && Parameters.Chance < 100 && !ConditionUtility.CheckProbability(Parameters.Chance))
            {
                return;
            }

            var buff = BuffProvider.Create(Caster, target, Parameters);

            if (!buff.CanBeApplied()) return;

            target.BuffController.AddBuff(buff);
        }
    }
}
