using UnityEngine;

namespace Level
{
    public class Background : MonoBehaviour
    {
        [SerializeField]
        protected Transform _target;

        void Start()
        {
            gameObject.transform.SetParent(_target);
        }
    }
}