using Spine.Unity;

namespace Core.Animation
{
    internal abstract class AnimationController
    {
        protected const int DefaultAnimationDuration = 1;

        protected AnimationController(SkeletonAnimation skeletonAnimation, ILogger logger)
        {
            SkeletonAnimation = skeletonAnimation;
            Logger = logger;
        }

        protected ILogger Logger { get; private set;}

        protected SkeletonAnimation SkeletonAnimation { get; private set; }
    }
}
