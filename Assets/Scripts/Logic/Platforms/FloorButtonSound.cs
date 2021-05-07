using System;
using UnityEngine;

namespace Logic.Platforms
{
    [RequireComponent(typeof(FloorButton))]
    public class FloorButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _sound;
        private FloorButton _button;

        private void Awake() => 
            _button = GetComponent<FloorButton>();

        private void OnEnable()
        {
            _button.Pressed += OnButtonPressed;
            _button.Realized += OnButtonPressed;
        }

        private void OnDisable()
        {
            _button.Pressed -= OnButtonPressed;
            _button.Realized -= OnButtonPressed;
        }

        private void OnButtonPressed() => 
            Instantiate(_sound, transform.position, Quaternion.identity);
    }
}