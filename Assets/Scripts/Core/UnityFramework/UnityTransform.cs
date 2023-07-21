using UnityEngine;

namespace Core.UnityFramework
{
    internal class UnityTransform : ITransform
    {
        private readonly Transform _transform;

        public UnityTransform(Transform transform)
        {
            _transform = transform;
        }

        public Vector3 LocalScale
        {
            get { return _transform.localScale; }
            set { _transform.localScale = value; }
        }
    }
}