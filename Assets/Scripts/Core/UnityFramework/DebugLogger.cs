using UnityEngine;

namespace Core.UnityFramework
{
    public class DebugLogger : ILogger
    {
        private readonly string _unitIdentifier;
        public DebugLogger(string unitIdentifier)
        {
            _unitIdentifier = unitIdentifier;
        }

        public void LogInfo(object obj)
        {
            Debug.Log(_unitIdentifier + ": " + obj);
        }

        public void LogWarning(object obj)
        { 
            Debug.LogWarning(_unitIdentifier + ": " + obj);
        }
    }
}