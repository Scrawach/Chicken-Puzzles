using Movement;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Chicken))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Falling))]
    public class MovementFreezer : MonoBehaviour
    {
        private Mover _mover;
        private Falling _falling;
        private Chicken _input;

        private bool _isFalling;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _falling = GetComponent<Falling>();
            _input = GetComponent<Chicken>();
        }

        private void OnEnable()
        {
            _mover.MoveStarted += OnMoveStarted;
            _mover.MoveEnded += OnMoveEnded;
            _falling.Fell += OnFell;
            _falling.Grounded += OnGrounded;
        }
        
        private void OnDisable()
        {
            _mover.MoveStarted -= OnMoveStarted;
            _mover.MoveEnded -= OnMoveEnded;
            _falling.Fell -= OnFell;
            _falling.Grounded -= OnGrounded;
        }
        
        private void OnMoveStarted() =>
            Freeze();

        private void OnMoveEnded() =>
            Unfreeze();

        private void OnFell()
        {
            _isFalling = true;
            Freeze();
        }
        
        private void OnGrounded()
        {
            _isFalling = false;
            Unfreeze();
        }

        private void Freeze() => 
            _input.Disable();

        private void Unfreeze()
        {
            if (_isFalling) 
                return;
            
            _input.Enable();
        }
    }
}