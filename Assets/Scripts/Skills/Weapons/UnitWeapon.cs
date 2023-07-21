using System.Linq;
using Core.Animation;
using Core.UnityFramework;
using Skills.Parameters;
using Stats;

namespace Skills.Weapons
{
    public class UnitWeapon : Weapon, IUnitWeapon
    {
        private readonly ISkillBehaviorProvider _mobSkillBehaviorProvider;

        public UnitWeapon(
            ISkillBehaviorProvider mobSkillBehaviorProvider,
            ISkillAnimationController skillAnimationController,
            ISkillParameters[] skillParameters)
            : base(skillAnimationController)
        {
            _mobSkillBehaviorProvider = mobSkillBehaviorProvider;
            SkillParameters = skillParameters;
        }

        public ISkillParameters[] SkillParameters { get; private set; }
        
        public bool IsSkillReady(ISkillParameters skillParameters)
        {
            return skillParameters.General.SkillCooldownCollection.All(p => p.IsReady());
        }

        public bool UseSkill(ISkillParameters skillParameters, IStats target)
        {
            if (!IsSkillReady(skillParameters))
            {
                return false;
            }
            
            var behavior = _mobSkillBehaviorProvider.GetSkillBehavior(
                skillParameters.BehaviorParameters, target);
            StartSkillActivation(behavior, skillParameters);
            return true;
        }
    }
}
