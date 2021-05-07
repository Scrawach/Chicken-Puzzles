using System;
using System.Collections;
using UnityEngine;

namespace Movement
{
    public class Mover : MonoBehaviour
    {
        private const float CheckWallDistance = .15f;
        
        [SerializeField] private float _speed = 5f;
        [SerializeField] private LayerMask _stoppingZone;

        private bool _isMoving;
        private Coroutine _moving;

        public bool IsMoving => _isMoving;

        public event Action MoveStarted;
        public event Action MoveEnded;
        
        public void StartMove(Vector3 direction)
        {
            if (CanNotMove(to: direction))
                return;
            
            if (_moving != null)
                StopCoroutine(_moving);
            
            _moving = null;
            _isMoving = true;
            MoveStarted?.Invoke();
            StartCoroutine(Moving(direction, _speed));
        }
        
        public void Stop()
        {
            if (_moving != null)
                StopCoroutine(_moving);

            _moving = null;
            _isMoving = false;
        }
    
        private bool CanNotMove(Vector3 to)
        {
            var checkWallRay = new Ray(transform.position + CheckWallDistance * Vector3.up, to);
            return Physics.Raycast(checkWallRay, 1f, _stoppingZone);
        }
        
        private IEnumerator Moving(Vector3 direction, float speed)
        {
            var startPoint = transform.position;
            var targetPoint = (startPoint + direction).Round();

            _moving = StartCoroutine(Smooth.Change(startPoint, targetPoint, speed, SmoothPositionChange));
            yield return _moving;
            _isMoving = false;
            MoveEnded?.Invoke();
        }

        protected virtual void SmoothPositionChange(Vector3 start, Vector3 end, float t) => 
            transform.position = Vector3.Lerp(start, end, t);
    }
}