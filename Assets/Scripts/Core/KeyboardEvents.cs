using System;
using Core.UnityFramework;
using UnityEngine;

namespace Core
{
    public class KeyboardEvents : IDisposable
    {
        private readonly IUnityUpdateEvents _unityUpdateEvents;

        private readonly string[] _keys = new[]
        {
            "z", "x", "c", "v", "b", "n"
        };
        public KeyboardEvents(IUnityUpdateEvents unityUpdateEvents)
        {
            _unityUpdateEvents = unityUpdateEvents;
            _unityUpdateEvents.UpdateFired += OnUpdate;
        }

        public event Action<string> KeyDown;
        public event Action<string> KeyUp;

        private void OnUpdate(float deltatime)
        {
            foreach (var key in _keys)
            {
                if (Input.GetKeyDown(key))
                {
                    OnDown(key);
                }
                else if (Input.GetKeyUp(key))
                {
                    OnUp(key);
                }
            }
        }

        private void OnDown(string key)
        {
            var handler = KeyDown;
            if (handler != null)
            {
                handler(key);
            }
        }

        private void OnUp(string key)
        {
            var handler = KeyUp;
            if (handler != null)
            {
                handler(key);
            }
        }

        public void Dispose() { _unityUpdateEvents.UpdateFired -= OnUpdate; }
    }
}
