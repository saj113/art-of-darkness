using Core.Animation;
using Skills.Weapons;
using UnitControllers;

namespace Stats
{
    public class CharacterStats : Stats, ICharacterStats
    {
        public CharacterStats(
            ICharacteristics characteristics, 
            ICharacterStateController characterStateController, 
            IAbsorbingBarrierController absorbingBarrierController, 
            IBuffController buffController, 
            IAcolyteController acolyteController,
            IStateAnimationController stateAnimationController, 
            IUnitGameObjectController unitGameObjectController, 
            ICharacterWeapon characterWeapon,
            IAgrController agrController) 
            : base(characteristics, characterStateController, absorbingBarrierController,
                  buffController, acolyteController, stateAnimationController, 
                  unitGameObjectController, agrController)
        {
            CharacterWeapon = characterWeapon;
        }

        public ICharacterWeapon CharacterWeapon { get; private set; }

        public override void Dispose()
        {
            base.Dispose();
            CharacterWeapon.Dispose();
        }
    }
}
