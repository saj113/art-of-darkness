using System;
using UnityEngine;

namespace Core.UnityFramework
{
    public interface IGameObject
    {
        event Action Destroyed;
        Vector2 Position { get; set; }
        void Destroy();
        bool IsVectorOnCollider(Vector2 vector);
        void Rotatetion(float x, float y, float z);
    }
}
