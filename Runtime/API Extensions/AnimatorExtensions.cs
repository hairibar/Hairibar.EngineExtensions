using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class AnimatorExtensions
    {
        public static bool IsPlayingAnimation(this Animator animator, string animationName, int layer = 0)
        {
            return animator.GetCurrentAnimatorStateInfo(layer).IsName(animationName);
        }

        public static bool AnimationIsFinished(this Animator animator, int layer = 0)
        {
            return animator.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1;
        }

        public static bool AnimationIsFinished(this Animator animator, string animationName, int layer = 0)
        {
            return animator.IsPlayingAnimation(animationName, layer) && animator.AnimationIsFinished(layer);
        }
    }
}
