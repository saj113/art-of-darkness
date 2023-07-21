using Core.Animation;
using Core.UnityFramework;
using UnitControllers;
using UnitControllers.AcolytesBehavior;
using UnityEngine;
using Utilities;

namespace Stats
{
    public delegate void UnitAgrChangedEvent(ICharacteristics target, int amount);
    public class UnitStats : Stats, IUnitStats
    {
        private readonly ITransform _healthBarTransform;
        private readonly float _healthBarMaxScaleX;

        public UnitStats(
            ICharacteristics characteristics,
            IUnitStateController unitStateController, 
            IAbsorbingBarrierController absorbingBarrierController, 
            IBuffController buffController, 
            IAcolyteController acolyteController,
            IStateAnimationController stateAnimationController, 
            IUnitGameObjectController unitGameObjectController,
            IFollowingController followingController, 
            ITransform healthBarTransform,
            IAgrController agrController) 
            : base(characteristics, unitStateController, absorbingBarrierController, 
                  buffController, acolyteController, stateAnimationController, unitGameObjectController,
                  agrController)
        {
            UnitController = unitStateController;
            FollowingController = followingController;
            _healthBarTransform = healthBarTransform;
            characteristics.HealthChanged += CharacteristicsOnHealthChanged;

            _healthBarMaxScaleX = healthBarTransform.LocalScale.x;
        }

        public event UnitAgrChangedEvent AgrChanged;

        public IUnitStateController UnitController { get; private set; }
        public IFollowingController FollowingController { get; private set; }
      
        public void ChangeAgr(ICharacteristics target, int amount)
        {
            OnAgrChanged(target, amount);
        }

        private void CharacteristicsOnHealthChanged(ICharacteristics characteristics, int amount)
        {
            if (amount < 0)
            {
                ChangeAgr(characteristics, amount * -1);
            }

            var difference = (float)characteristics.Health / (float)characteristics.MaxHealth;
            var coefficient = ValueUtility.CalculatePercent(_healthBarMaxScaleX, difference * 100);
            _healthBarTransform.LocalScale = new Vector3(
                coefficient,
                _healthBarTransform.LocalScale.y,
                _healthBarTransform.LocalScale.z);
        }

        public override void Dispose()
        {
            Characteristics.HealthChanged -= CharacteristicsOnHealthChanged;
            FollowingController.Dispose();
            base.Dispose();
        }

        private void OnAgrChanged(ICharacteristics target, int amount)
        {
            var handler = AgrChanged;
            if (handler != null)
            {
                handler(target, amount);
            }
        }
    }
}
