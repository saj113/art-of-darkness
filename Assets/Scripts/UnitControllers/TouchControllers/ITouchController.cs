using System;
using UnityEngine;

namespace UnitControllers.TouchControllers
{
    internal interface ITouchController : IDisposable
    {
        void Disable();
        void Enable();
        ShapeType TryRecognize();
        float GetLastCenterPoint();
    }
}