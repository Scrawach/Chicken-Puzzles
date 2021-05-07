using System;
using UnityEngine;

namespace Logic.Platforms
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(FloorButton))]
    public class FloorButtonAnimator : MonoBehaviour
    {
        private static readonly int PushHash = Animator.StringToHash("Push");
        private static readonly int RealizeHash = Animator.StringToHash("Realize");

        private Animator _animator;
        private FloorButton _button;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _button = GetComponent<FloorButton>();
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
            ResetAnimations();
            _animator.SetTrigger(PushHash);
        }

        private void OnButtonRealized()
        {
            ResetAnimations();
            _animator.SetTrigger(RealizeHash);
        }

        private void ResetAnimations()
        {
            _animator.ResetTrigger(PushHash);
            _animator.ResetTrigger(RealizeHash);
        }
    }
}