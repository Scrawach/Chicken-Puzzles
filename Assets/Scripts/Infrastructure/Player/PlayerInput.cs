using System;
using System.Collections;
using Infrastructure.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Infrastructure.Player
{
    public class PlayerInput : MonoSingleton<PlayerInput>
    {
        private const float ValidMoveDistance = 5f;
        
        public event Action Began;
        public event Action Fired;
        public event Action<Vector3> Moved;

        private bool _isBlocked;
        private float _cameraAngle;
        private Vector2 _startTouchPosition;
        private bool _isMoved;

        protected override void Awake()
        {
            base.Awake();
            _cameraAngle = Camera.main.transform.rotation.y;
        }
        
        private void Update()
        {
            ReadInput();
        }

        public void Enable() => 
            _isBlocked = false;

        public void Disable() => 
            _isBlocked = true;
        

        private void ReadInput()
        {
            if (_isBlocked)
                return;

#if UNITY_EDITOR || !UNITY_ANDROID
            ReadKeyboard();
#else
            ReadTouch();
#endif
        }

        private void ReadTouch()
        {
            if (Input.touchCount <= 0)
                return;

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Began?.Invoke();
                    break;
                case TouchPhase.Moved:
                    if (_isMoved)
                        break;

                    if (NotEnoughDistance(touch))
                        break;

                    _startTouchPosition = touch.position;
                    _isMoved = true;
                    break;
                case TouchPhase.Ended:
                    TouchDecode(touch);
                    break;
            }
        }

        private void ReadKeyboard()
        {
            if (Input.GetButtonDown("Left"))
                Moved?.Invoke(Vector3.left);
            else if (Input.GetButtonDown("Right"))
                Moved?.Invoke(Vector3.right);
            else if (Input.GetButtonDown("Up"))
                Moved?.Invoke(Vector3.forward);
            else if (Input.GetButtonDown("Down"))
                Moved?.Invoke(Vector3.back);
            else if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
                Fired?.Invoke();
        }

        private static bool NotEnoughDistance(Touch touch) => 
            touch.deltaPosition.magnitude <= ValidMoveDistance;

        private void TouchDecode(Touch touch)
        {
            if (_isMoved == false)
            {
                Fired?.Invoke();
                return;
            }

            _isMoved = false;
            
            var delta = touch.position - _startTouchPosition;
            var direction = ConvertToWorldVector(delta);
            var movement = MovementDirection(direction);
            Moved?.Invoke(movement);
        }

        private Vector2 ConvertToWorldVector(Vector2 screenDelta) =>
            RotateVector(screenDelta.normalized, 360f - _cameraAngle);

        private static Vector3 MovementDirection(Vector2 delta)
        {
            var absDelta = new Vector2(Mathf.Abs(delta.x), Mathf.Abs(delta.y));

            if (absDelta.x > absDelta.y)
                return delta.x > 0 ? Vector3.right : Vector3.left;

            return delta.y > 0 ? Vector3.forward : Vector3.back;
        }

        private static Vector2 RotateVector(Vector2 target, float angle)
        {
            var xPos = target.x * Mathf.Cos(Deg2Rad(angle)) - target.y * Mathf.Sin(Deg2Rad(angle));
            var yPos = target.y * Mathf.Cos(Deg2Rad(angle)) + target.x * Mathf.Sin(Deg2Rad(angle));
            return new Vector2(xPos, yPos).normalized; 
        }

        private static float Deg2Rad(float degrees) =>
            degrees * Mathf.Deg2Rad;
    }
}