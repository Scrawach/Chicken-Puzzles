using System;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Rigidbody))]
    public class Falling : MonoBehaviour
    {
        [SerializeField] private float _force = 10f;

        private Mover _mover;
        private Rigidbody _body;
        private bool _isGrounded;
        
        public event Action Fell;
        public event Action Grounded;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _body = GetComponent<Rigidbody>();
        }

        private void Start() => 
            StartFalling();

        private void OnCollisionEnter(Collision other)
        {
            _isGrounded = true;

            _mover.Stop();
            Grounded?.Invoke();
        }

        private void OnCollisionExit(Collision other)
        {
            _isGrounded = false;
        }

        private void OnEnable() => 
            _mover.MoveEnded += OnMoveEnded;

        private void OnDisable() => 
            _mover.MoveEnded -= OnMoveEnded;

        private void OnMoveEnded()
        {
            if (_isGrounded)
                return;
            
            StartFalling();
            Fell?.Invoke();
        }

        private void StartFalling() => 
            _body.velocity = _force * Vector3.down;
    }
}