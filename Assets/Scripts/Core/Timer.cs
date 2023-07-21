using UnityEngine;

namespace Core
{
    internal class Timer
    {
        private readonly int _chargeTime;
        private float _timeElapsed;

        public Timer(int chargeTime, bool initialChargetState = true)
        {
            _chargeTime = chargeTime;

            if (initialChargetState)
            {
                _timeElapsed = chargeTime;
            }
        }

        public bool IsCharged()
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed > _chargeTime)
            {
                _timeElapsed = 0;
                return true;
            }

            return false;
        }
    }
}
