using UnityEngine;

namespace Core.Prefabs
{
    public interface ISkillSoundPrefabProvider
    {
        AudioClip GetShadowBoltsEventClip();
        AudioClip GetShadowBoltsStartClip();
        AudioClip GetShadeEventClip();
        AudioClip GetShadeStartClip();
        AudioClip GetShadowJumpStartClip();
        AudioClip GetDrainLifeEventClip();
        AudioClip GetDrainLifeStartClip();
        AudioClip GetResurrectEventClip();
        AudioClip GetResurrectStartClip();
    }
}