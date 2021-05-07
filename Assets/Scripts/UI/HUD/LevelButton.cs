using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    [RequireComponent(typeof(Button))]
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Image _unlockedImage;
        [SerializeField] private Image _lockedImage;
        
        [SerializeField] private Color _fullOpenColor;

        private Button _button;
        private int _buildIndex;
        
        public bool IsLocked { get; private set; }

        public event Action<int> Clicked;

        public void Construct(int index, bool isLocked)
        {
            _buildIndex = index;
            IsLocked = isLocked;
            
            _button = GetComponent<Button>();
            
            _unlockedImage.gameObject.SetActive(false);
            _lockedImage.gameObject.SetActive(true);

            if (isLocked == false)
                Unlock();
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked() => 
            Clicked?.Invoke(_buildIndex);

        public void Unlock()
        {
            _unlockedImage.gameObject.SetActive(true);
            _lockedImage.gameObject.SetActive(false);
            _label.text = _buildIndex.ToString();
            IsLocked = false;
        }

        public void UnlockPassedStar()
        {
            //_levelStar.sprite = _passedSprite;
        }

        public void UnlockStepStar()
        {
            _unlockedImage.color = _fullOpenColor;
        }
    }
}