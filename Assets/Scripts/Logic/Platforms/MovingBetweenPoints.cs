using System.Collections;
using Movement;
using UnityEngine;

namespace Logic.Platforms
{
    [RequireComponent(typeof(Mover))]
    public class MovingBetweenPoints : MovingPlatform
    {
        [SerializeField] private float _pauseInSeconds;
        [SerializeField] private Transform[] _points;

        private Mover _mover;
        private IEnumerator _pointsPicker;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _pointsPicker = _points.GetEnumerator();
        }

        private void Start() => 
            StartCoroutine(MoveWithPause(_pauseInSeconds));

        private void OnEnable() => 
            _mover.MoveEnded += OnMoveEnded;

        private void OnDisable() => 
            _mover.MoveEnded -= OnMoveEnded;


        private void OnMoveEnded() =>
            StartCoroutine(MoveWithPause(_pauseInSeconds));

        private IEnumerator MoveWithPause(float pause)
        {
            yield return new WaitForSeconds(pause);
            Move();
        }
        
        private void Move()
        {
            var direction = NextPosition() - transform.position;
            _mover.StartMove(direction);
        }
        
        private Vector3 NextPosition()
        {
            if (_pointsPicker.MoveNext() == false)
            {
                _pointsPicker.Reset();
                _pointsPicker.MoveNext();
            }

            return ((Transform) _pointsPicker.Current)?.position ?? Vector3.zero;
        }
    }
}