using System;
using Stats;
using UnityEngine;

namespace UnitControllers
{
    public interface IUnitGameObjectController
    {
        event Action Destroyed;
        Vector2 Position { get; set; }
        Vector2 CenterPosition { get; }
        Bounds Bounds { get; } 
        void Destroy();
        void AddChild(Transform transform);
        void SetActive(bool isActive);
        void TurnUnit(int direction);
        void TurnUnit(float to);
        void Move(float velocity, int direction);
    }
}