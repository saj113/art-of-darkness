using Core;
using UnityEngine;

namespace UnitControllers
{
    public static class Extensions
    {
        public static bool IsCollision(this IUnitGameObjectController gameObjectController, Bounds bounds)
        {
            var current = gameObjectController.Bounds;
            return (double) current.min.x <= (double) bounds.max.x && (double) current.max.x >= (double) bounds.min.x &&
                   ((double) current.min.y <= (double) bounds.max.y && (double) current.max.y >= (double) bounds.min.y);
        }
        
        public static bool IsCollision(this IUnitGameObjectController gameObjectController, float x)
        {
            var current = gameObjectController.Bounds;
            return current.min.x <= (double)x && current.max.x >= (double) x;
        }

        public static float GetDistanceTo(this IUnitGameObjectController gameObjectController, float to)
        {
            return gameObjectController.Position.x < to
                ? gameObjectController.Bounds.max.x.Distance(to)
                : gameObjectController.Bounds.min.x.Distance(to);
        }
        
        public static float GetDistanceTo(this IUnitGameObjectController gameObjectController, Bounds to)
        {
            return gameObjectController.Position.x < to.center.x
                ? gameObjectController.Bounds.max.x.Distance(to.min.x)
                : gameObjectController.Bounds.min.x.Distance(to.max.x);
        }
        
        public static float GetPositionFor(this IUnitGameObjectController gameObjectController, float forX, float distance)
        {
            return gameObjectController.Position.x < forX
                ? gameObjectController.Bounds.max.x + distance
                : gameObjectController.Bounds.max.x - distance;
        }
    }
}