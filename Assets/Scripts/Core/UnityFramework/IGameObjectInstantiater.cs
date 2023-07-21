using UnityEngine;

namespace Core.UnityFramework
{
    public interface IGameObjectInstantiater
    {
        T Instantiate<T>(T monoBehaviour) where T : Component;
        T Instantiate<T>(T monoBehaviour, Vector2 position) where T : Component;
        T Instantiate<T>(T monoBehaviour, bool isEnabled) where T : Component;
        T TryInstantiate<T>(T monoBehaviour, Vector2 position) where T : Component;
    }
}
