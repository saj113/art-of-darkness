using System;
using UnityEngine;

namespace Utilities
{
    public class TouchPointDetecter : MonoBehaviour
    {
        private Action<Vector2> _onTouchedCallback;
        private Plane _plane;

        public void Subscribe(Vector3 startPosition, Action<Vector2> onTouchedCallback)
        {
            _plane = new Plane(-Vector3.forward, startPosition);
            _onTouchedCallback = onTouchedCallback;
        }

        public void Unscribe()
        {
            _onTouchedCallback = null;
        }

        void Update()
        {
            if (_onTouchedCallback == null)
            {
                return;
            }

            // Handle native touch events
            foreach (var touch in Input.touches)
            {
                HandleTouch(UnityEngine.Camera.main.ScreenPointToRay(touch.position));
            }

            // Simulate touch events from mouse events
            if (Input.touchCount == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    HandleTouch(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition));
                }
            }
        }

        private void HandleTouch(Ray ray)
        {
            float distance;
            if (_plane.Raycast(ray, out distance))
            {
                var touchPosition = new Vector2(ray.GetPoint(distance).x, ray.GetPoint(distance).y);
                OnTouched(touchPosition);
            }
        }

        protected virtual void OnTouched(Vector2 obj)
        {
            var handler = _onTouchedCallback;
            if (handler != null)
            {
                _onTouchedCallback(obj);
            }
        }
    }
}
