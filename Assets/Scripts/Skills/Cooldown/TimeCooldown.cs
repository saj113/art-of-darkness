using Core;
using Core.UnityFramework;

namespace Skills.Cooldown
{
    public class TimeCooldown : ISkillCooldown, ITimeCooldown
    {
        private readonly IUnityUpdateEvents _updateEvents;
        private readonly float _recoveryTime;
        private float _currentCooldownTime;

        public TimeCooldown(IUnityUpdateEvents updateEvents, float recoveryTime)
        {
            _updateEvents = updateEvents;
            _updateEvents.FixedUpdateFired += UpdateEventsOnFixedUpdateFired;
            _recoveryTime = recoveryTime;
            Contract.Ensure(_recoveryTime > 0, "Required cooldown time has invalid value");
        }

        public SkillCooldownType Type => SkillCooldownType.Time;

        public float CurrentCooldown => _currentCooldownTime;
        public float RequiredCooldown => _recoveryTime;

        public bool IsReady()
        {
            return _currentCooldownTime < 0.01f;
        }

        public void GiveCost()
        {
            _currentCooldownTime = _recoveryTime;
        }

        public void Dispose()
        {
            _updateEvents.FixedUpdateFired -= UpdateEventsOnFixedUpdateFired;
        }

        private void UpdateEventsOnFixedUpdateFired(float deltaTime)
        {
            if (_currentCooldownTime > 0.01f)
            {
                _currentCooldownTime -= deltaTime;
            }
        }
    }
}