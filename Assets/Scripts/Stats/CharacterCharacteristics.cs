using System;
using Core.UnityFramework;
using UnitControllers;

namespace Stats
{
    public class CharacterCharacteristics : Characteristics
    {
        private readonly IUnityUpdateEvents _unityUpdateEvents;
        
        public CharacterCharacteristics(
            Guid unitId,
            Tag tag,
            int health,
            int power,
            float speed,
            IAbsorbingBarrierController absorbingBarrierController,
            IUnityUpdateEvents updateEvents)
            : base(
                unitId,
                tag,
                health,
                power,
                speed,
                absorbingBarrierController)
        {
            Mana = 100;
            _unityUpdateEvents = updateEvents;
            _unityUpdateEvents.EverySecond += EverySecond;
        }
        
        public override void Dispose()
        {
            _unityUpdateEvents.EverySecond -= EverySecond;
        }
        
        private void EverySecond()
        {
            //ChangeStat(StatAttribute.Mana, ConstantUtility.ManaRegeneration);
        }
    }
}
