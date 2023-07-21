using Stats;
using Stats.Data;
using UnityEngine;

namespace CameraScripts
{
    public abstract class AbstractTargetFollower : MonoBehaviour
    {
        public enum UpdateType // The available methods of updating are:
        {
            FixedUpdate, // Update in FixedUpdate (for tracking rigidbodies).
            LateUpdate, // Update in LateUpdate. (for tracking objects that are moved in IsBuffCanDelete)
            ManualUpdate, // user must call to update camera
        }

        [SerializeField]
        protected Transform m_Target;            // The target object to follow
        [SerializeField]
        private bool m_AutoTargetPlayer = true;  // Whether the rig should automatically target the player.
        [SerializeField]
        private UpdateType m_UpdateType;         // stores the selected update type

        private ICharacteristics _targetCharacteristics;


        protected virtual void Start()
        {
            // if auto targeting is used, find the object tagged "Player"
            // any class inheriting from this should call base.Start() to perform this action!
            if (m_AutoTargetPlayer)
            {
                FindAndTargetPlayer();
            }
            if (m_Target == null) return;
            _targetCharacteristics = m_Target.GetComponent<CharacterStatsData>().Stats.Characteristics;
        }


        private void FixedUpdate()
        {
            // we update from here if updatetype is set to Fixed, or in auto mode,
            // if the target has a rigidbody, and isn't kinematic.
            if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
            {
                FindAndTargetPlayer();
            }
            if (m_UpdateType == UpdateType.FixedUpdate)
            {
                FollowTarget(_targetCharacteristics, Time.deltaTime);
            }
        }

        protected abstract void FollowTarget(ICharacteristics targetCharacteristics, float deltaTime);


        public void FindAndTargetPlayer()
        {
            // auto target an object tagged player, if no target has been assigned
            var targetObj = GameObject.FindGameObjectWithTag("Player");
            if (targetObj)
            {
                SetTarget(targetObj.transform);
            }
        }


        public virtual void SetTarget(Transform newTransform)
        {
            m_Target = newTransform;
        }
    }
}
