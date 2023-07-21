using UnityEngine;

namespace Core.UnityFramework
{
    public class GameObjectInstantiater : IGameObjectInstantiater
    {
        public TType Instantiate<TType>(TType monoBehaviour) where TType : Component
        {
            return Object.Instantiate(monoBehaviour);
        }

        public TType Instantiate<TType>(TType monoBehaviour, bool isEnabled) where TType : Component
        {
            var instance = Object.Instantiate(monoBehaviour);
            instance.gameObject.SetActive(isEnabled);
            return instance;
        }

        public T Instantiate<T>(T monoBehaviour, Vector2 position) where T : Component
        {
            var instance = Object.Instantiate(monoBehaviour);
            instance.transform.position = position;
            return instance;
        }

        public TType TryInstantiate<TType>(TType monoBehaviour, Vector2 position) where TType : Component
        {
            if (monoBehaviour == null)
            {
                return null;
            }

            var instance = Object.Instantiate(monoBehaviour);
            instance.transform.position = position;
            return instance;
        }
    }
}
