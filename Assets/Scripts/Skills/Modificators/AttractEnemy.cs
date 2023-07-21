using Stats;

namespace Skills.Modificators
{
    public class AttractEnemy : Modificator
    {
        private readonly int _agrFactorMultiplication;

        public AttractEnemy(ISkillCaster caster, int agrFactorMultiplication) 
            : base(caster, TargetUnitRelation.Enemy)
        {
            _agrFactorMultiplication = agrFactorMultiplication;
        }

        protected override void ApplyChanges(IStats target)
        {
            var unitStats = target as IUnitStats;
            if (unitStats != null)
                unitStats.ChangeAgr(
                    Caster.Characteristics, Caster.Characteristics.Power * _agrFactorMultiplication);
        }
    }
}
