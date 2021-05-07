using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Animator))]
    public class GateAnimator : MonoBehaviour
    {
        private static readonly int OpenHash = Animator.StringToHash("Open");
        
        private Animator _animator;
        
        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void PlayOpen() => 
            _animator.SetTrigger(OpenHash);
    }
}
