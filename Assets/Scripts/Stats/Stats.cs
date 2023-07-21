using Core.Animation;
using UnitControllers;

namespace Stats
{
    public abstract class Stats : IStats
    {
        protected Stats(
            ICharacteristics characteristics,
            IStateController stateController,
            IAbsorbingBarrierController absorbingBarrierController, 
            IBuffController buffController, 
            IAcolyteController acolyteController,
            IStateAnimationController stateAnimationController, 
            IUnitGameObjectController unitGameObjectController,
            IAgrController agrController)
        {
            StateController = stateController;
            AbsorbingBarrierController = absorbingBarrierController;
            BuffController = buffController;
            AcolyteController = acolyteController;
            StateAnimationController = stateAnimationController;
            GameObjectController = unitGameObjectController;
            Characteristics = characteristics;
            AgrController = agrController;
            Characteristics.Died += OnUnitDie;
            StateController.SetIdleState();
        }

        public ICharacteristics Characteristics { get; private set; }

        public IUnitGameObjectController GameObjectController { get; private set; }

        public IStateController StateController { get; private set; }

        public IStateAnimationController StateAnimationController { get; private set; }

        public IBuffController BuffController { get; private set; }

        public IAbsorbingBarrierController AbsorbingBarrierController { get; private set; }

        public IAcolyteController AcolyteController { get; private set; }

        public IAgrController AgrController { get; private set; }

        private void OnUnitDie(ICharacteristics characteristics)
        {
            StateController.SetDeadState();
        }

        public virtual void Dispose()
        {
            Characteristics.Died -= OnUnitDie;
            Characteristics.Dispose();
            StateController.Dispose();
            StateAnimationController.Dispose();
            BuffController.Dispose();
            AcolyteController.Dispose();
        }
    }
}
