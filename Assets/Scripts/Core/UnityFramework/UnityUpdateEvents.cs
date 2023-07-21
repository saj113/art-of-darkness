using System;
using UnityEngine;

namespace Core.UnityFramework
{
    public class UnityUpdateEvents : MonoBehaviour, IUnityUpdateEvents
    {
        public event Action<float> UpdateFired;
        public event Action<float> FixedUpdateFired;
        public event Action EverySecond;

        void Start()
        {
            InvokeRepeating("OnEverySecond", 1f, 1f);
        }

        void Update()
        {
            OnUpdateFired();
        }

        void FixedUpdate()
        {
            OnFixedUpdateFired();
        }

        protected virtual void OnFixedUpdateFired()
        {
            var handler = FixedUpdateFired;
            if (handler != null) handler(Time.deltaTime);
        }

        protected virtual void OnUpdateFired()
        {
            var handler = UpdateFired;
            if (handler != null) handler(Time.deltaTime);
        }

        protected virtual void OnEverySecond()
        {
            var handler = EverySecond;
            if (handler != null) handler();
        }
    }
}
