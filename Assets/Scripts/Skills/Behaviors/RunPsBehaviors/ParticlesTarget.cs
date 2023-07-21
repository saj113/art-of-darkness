using UnitControllers;
using UnityEngine;

namespace Skills.Behaviors.RunPsBehaviors
{
    public class ParticlesTarget
    {
        private readonly Vector2 _targetPosition;
        private readonly IUnitGameObjectController _unitGameObjectController;

        public ParticlesTarget(Vector2 vector)
        {
            _unitGameObjectController = null;
            _targetPosition = vector;
        }

        public ParticlesTarget(IUnitGameObjectController unitGameObjectController)
        {
            _unitGameObjectController = unitGameObjectController;
            _targetPosition = Vector2.zero;
        }

        public Vector2 GetTargetPosition()
        {
            if (_unitGameObjectController != null)
            {
                return _unitGameObjectController.CenterPosition;
            }

            return _targetPosition;
        }
    }
}
