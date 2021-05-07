using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Image _imageHandler;
        [SerializeField] private Sprite _stateOn;
        [SerializeField] private Sprite _stateOff;
        
        private Button _button;
        private bool _toggle = true;

        public event Action<bool> Toggled;
        
        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnToggled);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnToggled);

        private void OnToggled()
        {
            SwitchToggleState();
            UpdateImage();
            Toggled?.Invoke(_toggle);
        }

        private void UpdateImage() => 
            _imageHandler.sprite = _toggle ? _stateOn : _stateOff;

        private void SwitchToggleState() => 
            _toggle = !_toggle;
    }
}