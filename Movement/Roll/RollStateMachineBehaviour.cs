using UnityEngine;

namespace Movement.Roll
{
    public class RollStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            var nextStateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            if (nextStateInfo.shortNameHash == stateInfo.shortNameHash) return;

            var rollAnimation = animator.GetComponent<RollAnimationMediator>();
            rollAnimation.OnExitedRoll();
        }
    }
}