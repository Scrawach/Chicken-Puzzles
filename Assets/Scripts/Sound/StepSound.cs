using Movement;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(Mover))]
    public class StepSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _stepSound;
        private Mover _mover;

        private void Awake() => 
            _mover = GetComponent<Mover>();

        private void OnEnable() => 
            _mover.MoveStarted += OnMoveEnded;

        private void OnDisable() => 
            _mover.MoveStarted -= OnMoveEnded;

        private void OnMoveEnded() => 
            Instantiate(_stepSound, transform.position, Quaternion.identity);
    }
}
