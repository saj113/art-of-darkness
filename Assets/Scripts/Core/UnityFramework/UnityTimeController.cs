using UnityEngine;

namespace Core.UnityFramework
{
    public class UnityTimeController : IUnityTimeController
    {
        public float DeltaTime
        {
            get { return Time.deltaTime; }
        }
    }
}
