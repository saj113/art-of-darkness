
namespace Core.Animation
{
    public class StateAnimations : IStateAnimations
    {

        public StateAnimations(
            string idle,
            string fall,
            string run,
            string stun,
            string standUp,
            float runTimeScale = 1f)
        {
            Idle = idle;
            Fall = fall;
            Run = run;
            Stun = stun;
            StandUp = standUp;
            RunTimeScale = runTimeScale;
        }

        public string Idle { get; private set; }
        public string Fall { get; private set; }
        public string Run { get; private set; }
        public string Stun { get; private set; }
        public string StandUp { get; private set; }
        public float RunTimeScale { get; private set; }
    }
}