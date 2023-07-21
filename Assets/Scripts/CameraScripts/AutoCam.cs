using System;
using Core;
using Level;
using Stats;
using UnityEngine;
using Utilities;

namespace CameraScripts
{
    public class AutoCam : AbstractTargetFollower
    {
        [SerializeField]
        private float m_LookForwardDistance = 2f;
        [SerializeField]
        private float m_MoveSpeed = 3; // How fast the rig will move to keep up with target's position
        [SerializeField]
        private float m_TurnSpeed = 1; // How fast the rig will turn to keep up with target's rotation
        [SerializeField]
        private float m_RollSpeed = 0.2f;// How fast the rig will roll (around Z axis) to match target's roll.
        [SerializeField]
        private float m_SpinTurnLimit = 90;// The threshold beyond which the camera stops following the target's rotation. (used in situations where a car spins out, for example)
        
        private float _mLastFlatAngle; // The relative angle of the target and the rig from the previous frame.
        private float _mCurrentTurnAmount; // How much to turn the camera
        private float _mTurnSpeedVelocityChange; // The change in the turn speed velocity
        private Vector3 _mRollUp = Vector3.up;// The roll of the camera around the z axis ( generally this will always just be up )
        private float _cameraMinX;
        private float _cameraMaxX;
        private ILevelPreferences _levelPreferences;

        protected override void Start()
        {
            base.Start();
            _levelPreferences = InstanceContainer.Instance.Resolve<ILevelPreferences>()
                .ThrowIfNull(nameof(_levelPreferences));
            _levelPreferences.WallsPositionChanged += UpdateMinMaxCameraPosition;
            UpdateMinMaxCameraPosition();
        }

        private void UpdateMinMaxCameraPosition()
        {
            var cam = Camera.main;
            var height = 2f * cam.orthographicSize;
            var width = height * cam.aspect;
            var middleOfWidth = width / 2.0f;
            _cameraMaxX = _levelPreferences.RightWall - middleOfWidth;
            _cameraMinX = _levelPreferences.LeftWall + middleOfWidth;
        }

        private void OnDestroy()
        {
            _levelPreferences.WallsPositionChanged -= UpdateMinMaxCameraPosition;
        }

        protected override void FollowTarget(ICharacteristics targetCharacteristics, float deltaTime)
        {
            // if no target, or no time passed then we quit early, as there is nothing to do
            if (!(deltaTime > 0) || m_Target == null || targetCharacteristics == null)
            {
                return;
            }

            // initialise some vars, we'll be modifying these in a moment
            var targetForward = m_Target.forward;
            var targetUp = m_Target.up;

            // we're in 'follow rotation' mode, where the camera rig's rotation follows the object's rotation.

            // This section allows the camera to stop following the target's rotation when the target is spinning too fast.
            // eg when a car has been knocked into a spin. The camera will resume following the rotation
            // of the target when the target's angular velocity slows below the threshold.
            var currentFlatAngle = Mathf.Atan2(targetForward.x, targetForward.z) * Mathf.Rad2Deg;

            var targetSpinSpeed = Mathf.Abs(Mathf.DeltaAngle(_mLastFlatAngle, currentFlatAngle)) / deltaTime;
            var desiredTurnAmount = Mathf.InverseLerp(m_SpinTurnLimit, m_SpinTurnLimit * 0.75f, targetSpinSpeed);
            var turnReactSpeed = (_mCurrentTurnAmount > desiredTurnAmount ? .1f : 1f);
            _mCurrentTurnAmount = Mathf.SmoothDamp(_mCurrentTurnAmount, desiredTurnAmount,
                                                      ref _mTurnSpeedVelocityChange, turnReactSpeed);
            _mLastFlatAngle = currentFlatAngle;

            var targetPositionX = m_Target.position.x +
                                  (m_LookForwardDistance * ValueUtility.GetDirection(targetCharacteristics));
            var targetPosition = new Vector3(
                Mathf.Clamp(targetPositionX, _cameraMinX, _cameraMaxX), 
                m_Target.position.y, 
                m_Target.position.z);

            // camera position moves towards target position:
            transform.position = Vector3.Lerp(transform.position, targetPosition, deltaTime * m_MoveSpeed);

            
            var rollRotation = Quaternion.LookRotation(targetForward, _mRollUp);

            // and aligning with the target object's up direction (i.e. its 'roll')
            _mRollUp = m_RollSpeed > 0 ? Vector3.Slerp(_mRollUp, targetUp, m_RollSpeed * deltaTime) : Vector3.up;
            transform.rotation = Quaternion.Lerp(transform.rotation, rollRotation, m_TurnSpeed * _mCurrentTurnAmount * deltaTime);
        }
    }
}
