using System;
using Infrastructure.Player;
using Movement;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Rotator))]
    [RequireComponent(typeof(Weapon))]
    [RequireComponent(typeof(RiseEffect))]
    [RequireComponent(typeof(ChickenAnimator))]
    public class Chicken : MonoBehaviour
    {
        private Mover _mover;
        private Rotator _rotor;
        private Weapon _weapon;
        private ChickenAnimator _animator;

        public event Action Moved;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _rotor = GetComponent<Rotator>();
            _weapon = GetComponent<Weapon>();
            _animator = GetComponent<ChickenAnimator>();
        }

        private void OnEnable()
        {
            PlayerInput.Instance.Began += OnBegan;
            PlayerInput.Instance.Fired += OnFired;
            PlayerInput.Instance.Moved += OnMoved;
        }

        private void OnDisable()
        {
            PlayerInput.Instance.Began -= OnBegan;
            PlayerInput.Instance.Fired -= OnFired;
            PlayerInput.Instance.Moved -= OnMoved;
        }
        
        private void OnBegan() => 
            _animator.PlayJumpPrep();

        private void OnFired()
        {
            _animator.PlayIdle();
            
            if (_weapon.CanShot())
                _weapon.Shot();
        }
        
        private void OnMoved(Vector3 direction)
        {
            if (_mover.IsMoving) 
                return;
            
            _animator.PlayIdle();
            _mover.StartMove(direction);
            _rotor.StartRotate(direction);
            Moved?.Invoke();
        }
        
        public void Enable() =>
            enabled = true;
        
        public void Disable() =>
            enabled = false;

        public void Kill()
        {
            GetComponent<RiseEffect>().Play();
            Destroy(gameObject);
        }
    }
}
