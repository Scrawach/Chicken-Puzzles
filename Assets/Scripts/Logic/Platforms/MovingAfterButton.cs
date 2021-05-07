using Movement;
using UnityEngine;

namespace Logic.Platforms
{
    [RequireComponent(typeof(Mover))]
    public class MovingAfterButton : MovingPlatform
    {
        [SerializeField] private FloorButton _button;
        [SerializeField] private Vector3 _moveDirection;

        private Mover _mover;
        private Vector3 _defaultPoint;

        private void Awake()
        {
            _defaultPoint = transform.localPosition;
            _mover = GetComponent<Mover>();
        }

        private void OnEnable()
        {
            _button.Pressed += OnButtonPressed;
            _button.Realized += OnButtonRealized;
        }
        
        private void OnDisable()
        {
            _button.Pressed -= OnButtonPressed;
            _button.Realized -= OnButtonRealized;
        }
        
        private void OnButtonPressed()
        {
            _mover.StartMove(_moveDirection);
        }

        private void OnButtonRealized()
        {
            var current = _defaultPoint - transform.localPosition;
            _mover.StartMove(current);
        }
    }
}