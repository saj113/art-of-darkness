using System;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;

namespace Core.UnityFramework
{
    public class UnityGameObject : IGameObject
    {
        private readonly GameObject _gameObject;

        public UnityGameObject(GameObject gameObject) { _gameObject = gameObject; }

        public event Action Destroyed;

        public Vector2 Position
        {
            get { return _gameObject.transform.position; }
            set { _gameObject.transform.position = value; }
        }

        public void Destroy()
        {
            Object.Destroy(_gameObject);
        }

        public bool IsVectorOnCollider(Vector2 vector)
        {
            var collider = _gameObject.GetComponent<Collider2D>();
            return ConditionUtility.IsVectorOnCollider(collider, vector);
        }

        void OnDestroy() { OnDestroyed(); }

        private void OnDestroyed()
        {
            var handler = Destroyed;
            if (handler != null)
            {
                handler();
            }
        }

        public void Rotatetion(float x, float y, float z)
        {
            _gameObject.transform.rotation.Set(x, y, z, 1);
        }
    }
}
