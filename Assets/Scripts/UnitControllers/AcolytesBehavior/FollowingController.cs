using Stats;

namespace UnitControllers.AcolytesBehavior
{
    internal class FollowingController : IFollowingController
    {
        private readonly ICharacteristics _characteristics;
        public FollowingController(ICharacteristics characteristics)
        {
            _characteristics = characteristics;
            _characteristics.Died += OnAcolyteIsDead;
        }
        public IFollowingInstructions FollowingInstructions { get; set; }

        public bool IsFolowerValid()
        {
            return FollowingInstructions != null && FollowingInstructions.IsFolowerValid();
        }

        public void OnAcolyteIsDead(ICharacteristics characteristics)
        {
            FollowingInstructions = null;
        }

        public void Dispose() { _characteristics.Died -= OnAcolyteIsDead; }
    }
}
