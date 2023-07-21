using UnityEngine;

namespace Core.Prefabs
{
    public class SkillSoundPrefabProvider : PrefabProvider, ISkillSoundPrefabProvider
    {
        private readonly AudioClip _shadowBoltsEventClip;
        private readonly AudioClip _shadowBoltsStartClip;
        private readonly AudioClip _shadeEventClip;
        private readonly AudioClip _shadeStartClip;
        private readonly AudioClip _shadowJumpStartClip;
        private readonly AudioClip _drainLifeEventClip;
        private readonly AudioClip _drainLifeStartClip;
        private readonly AudioClip _resurrectEventClip;
        private readonly AudioClip _resurrectStartClip;
        
        public SkillSoundPrefabProvider()
        {
            _shadowBoltsEventClip = GetPrefab<AudioClip>("Sounds/Character/ShadowBoltsEvent");
            _shadowBoltsStartClip = GetPrefab<AudioClip>("Sounds/Character/ShadowBoltsStart");
            _shadeEventClip = GetPrefab<AudioClip>("Sounds/Character/ShadeEvent");
            _shadeStartClip = GetPrefab<AudioClip>("Sounds/Character/ShadeStart");
            _shadowJumpStartClip = GetPrefab<AudioClip>("Sounds/Character/ShadowJumpStart");
            _drainLifeEventClip = GetPrefab<AudioClip>("Sounds/Character/DrainLifeEvent");
            _drainLifeStartClip = GetPrefab<AudioClip>("Sounds/Character/DrainLifeStart");
            _resurrectEventClip = GetPrefab<AudioClip>("Sounds/Character/ResurrectEvent");
            _resurrectStartClip = GetPrefab<AudioClip>("Sounds/Character/ResurrectStart");
        }
        
        public AudioClip GetShadowBoltsEventClip() { return _shadowBoltsEventClip; }
        public AudioClip GetShadowBoltsStartClip() { return _shadowBoltsStartClip; }
        public AudioClip GetShadeEventClip() { return _shadeEventClip; }
        public AudioClip GetShadeStartClip() { return _shadeStartClip; }
        public AudioClip GetShadowJumpStartClip() { return _shadowJumpStartClip; }
        public AudioClip GetDrainLifeEventClip() { return _drainLifeEventClip; }
        public AudioClip GetDrainLifeStartClip() { return _drainLifeStartClip; }
        public AudioClip GetResurrectEventClip() { return _resurrectEventClip; }
        public AudioClip GetResurrectStartClip() { return _resurrectStartClip; }
    }
}
