using Stats;

namespace Skills.Modificators
{
    public class ChangeCasterStat : Modificator
    {
        private readonly StatAttribute _attribute;
        private readonly int _power;

        public ChangeCasterStat(
            ISkillCaster caster, 
            TargetUnitRelation targetUnitRelation,
            StatAttribute attribute,
            int power) : base(caster, targetUnitRelation)
        {
            _attribute = attribute;
            _power = power;
        }

        protected override void ApplyChanges(IStats target)
        {
            Caster.Characteristics.ChangeStat(_attribute, _power);
        }
    }
}