using System.Collections.Generic;
using System.Linq;
using Core.UnityFramework;
using UnitControllers.TouchControllers.ShapesRecogntions;
using UnityEngine;

namespace UnitControllers.TouchControllers
{
    internal class TouchController : ITouchController
    {
        private readonly ISpecificPointFinder _specificPointFinder;
        private readonly IEnumerable<IShapeRecognizer> _shapeRecognizers;
        private readonly LineRenderer _lineRenderer;
        private readonly IUnityUpdateEvents _unityUpdateEvents;
        private bool _isMousePressed;
        private readonly HashSet<Vector3> _pointsList = new HashSet<Vector3>();
        private Vector3 _mousePos;
        private bool _isLineDrew;
        private float _cleareDelay;
        private bool _isEnabled;
        private bool _isResultReady;

        public TouchController(
            ISpecificPointFinder specificPointFinder,
            IEnumerable<IShapeRecognizer> shapeRecognizers, 
            IUnityUpdateEvents unityUpdateEvents, 
            LineRenderer lineRenderer)
        {
            _unityUpdateEvents = unityUpdateEvents;
            _specificPointFinder = specificPointFinder;
            _shapeRecognizers = shapeRecognizers;
            _lineRenderer = lineRenderer;
            _unityUpdateEvents.UpdateFired += UnityUpdateEventsOnUpdateFired;
            _isMousePressed = false;
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        public ShapeType TryRecognize()
        {
            var vectors = TryGetLineVectors();
            if (vectors == null || !vectors.Any())
            {
                return ShapeType.None;
            }

            var shapeSidePoints = _specificPointFinder.GetShapeSidePoints(vectors);
            foreach (var shapeRecognizer in _shapeRecognizers)
            {
                if (shapeRecognizer.Recognize(shapeSidePoints))
                {
                    return shapeRecognizer.ShapeType;
                }
            }

            return ShapeType.None;
        }

        public float GetLastCenterPoint()
        {
            return _specificPointFinder.GetLastCenterPoint();
        }

        private IEnumerable<Vector3> TryGetLineVectors()
        {
            if (!_isResultReady) return null;

            _isResultReady = false;
            return _pointsList;
        }

        private void UnityUpdateEventsOnUpdateFired(float f)
        {
            if (_isLineDrew)
            {
                ClearWithDelay();
            }

            if (!_isEnabled)
            {
                return;
            }

            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                _isMousePressed = _mousePos.y > 0;
                ClearLine();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isMousePressed = false;
                _isLineDrew = _pointsList.Any();
                _isResultReady = true;
            }

            if (_isMousePressed)
            {
                _mousePos.z = 0;
                if (!_pointsList.Contains(_mousePos))
                {
                    _pointsList.Add(_mousePos);
                    _lineRenderer.positionCount = _pointsList.Count;
                    _lineRenderer.SetPosition(_pointsList.Count - 1, _pointsList.Last());
                }
            }
        }

        private void ClearWithDelay()
        {
            _cleareDelay += Time.deltaTime;
            if (_cleareDelay > 0.3f)
            {
                ClearLine();
            }
        }

        private void ClearLine()
        {
            _cleareDelay = 0;
            _isLineDrew = false;
            _lineRenderer.positionCount = 0;
            _pointsList.Clear();
        }

        public void Dispose() { _unityUpdateEvents.UpdateFired -= UnityUpdateEventsOnUpdateFired; }
    }
}
