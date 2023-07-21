using Skills;
using UnitControllers;

namespace Stats
{
    public class SkillCaster : ISkillCaster
    {
        public SkillCaster(
            ICharacteristics characteristics, 
            IUnitGameObjectController gameObjectController,
            IAcolyteController acolyteController,
            IAgrController agrController,
            TargetUnitRelation targetUnitRelation)
        {
            Characteristics = characteristics;
            GameObjectController = gameObjectController;
            TargetRelation = targetUnitRelation;
            AcolyteController = acolyteController;
            AgrController = agrController;
        }
        
        public TargetUnitRelation TargetRelation { get; private set; }
        public ICharacteristics Characteristics { get; private set; }
        public IAcolyteController AcolyteController { get; private set; }
        public IUnitGameObjectController GameObjectController { get; private set; }
        public IAgrController AgrController { get; private set; }
    }
}