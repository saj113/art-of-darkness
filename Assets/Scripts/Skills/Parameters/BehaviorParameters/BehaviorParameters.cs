using Skills.Behaviors;
using Skills.Modificators;
using Stats;

namespace Skills.Parameters.BehaviorParameters
{
    public abstract class BehaviorParameters : IBehaviorParameters
    {
        private readonly IModificator[] _modificators;
        protected BehaviorParameters(SkillBehaviorType type, AnimationSkillParticles animationParticles, IModificator[] modificators)
        {
            AnimationParticles = animationParticles;
            _modificators = modificators;
            Type = type;
        }

        public SkillBehaviorType Type { get; private set; }
        public AnimationSkillParticles AnimationParticles { get; private set; }

        public void ApplyModificators(IStats target, bool applyBuffs)
        {
            foreach (var modificator in _modificators)
            {
                if (!applyBuffs && modificator.IsBuff)
                {
                    continue;
                }

                modificator.Apply(target);
            }
        }
    }
}
