using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Animator))]
    public class ChickenAnimator : MonoBehaviour
    {
        private static readonly int JumpPreparationHash = Animator.StringToHash("JumpPreparation");
        private static readonly int FlyOutHash = Animator.StringToHash("FlyOut");
        private static readonly int GroundedHash = Animator.StringToHash("Grounded");

        private Animator _animator;

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void PlayJumpPrep()
        {
            ResetTriggers();
            _animator.SetTrigger(JumpPreparationHash);
        }

        public void PlayIdle()
        {
            ResetTriggers();
            _animator.SetTrigger(GroundedHash);
        }

        public void PlayFlyOut()
        {
            ResetTriggers();
            _animator.SetTrigger(FlyOutHash);
        }

        private void ResetTriggers()
        {
            _animator.ResetTrigger(JumpPreparationHash);
            _animator.ResetTrigger(GroundedHash);
            _animator.ResetTrigger(FlyOutHash);
        }
    }
}
